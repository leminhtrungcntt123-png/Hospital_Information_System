using HIS.BLL.DTOs;
using HIS.DAL;
using HIS.Domain;
using Microsoft.EntityFrameworkCore;

namespace HIS.BLL;

public class AuthService : IAuthService
{
    public const int MaxFailedAttempts = 5;
    public const int LockoutMinutes = 15;

    private const string InvalidCredentialsMessage = "Tên đăng nhập hoặc mật khẩu không đúng.";

    private readonly AppDbContext _context;

    public AuthService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<LoginResultDto> LoginAsync(
        string username,
        string password,
        string? ipAddress = null,
        CancellationToken cancellationToken = default)
    {
        string trimmedUsername = (username ?? string.Empty).Trim();

        if (trimmedUsername.Length == 0 || string.IsNullOrEmpty(password))
        {
            return LoginResultDto.Fail("Vui lòng nhập tên đăng nhập và mật khẩu.");
        }

        Account? account = await _context.Accounts
            .Include(a => a.Role)
            .Include(a => a.Patient)
            .Include(a => a.Doctor)
            .Include(a => a.Nurse)
            .Include(a => a.DepartmentHead)
            .FirstOrDefaultAsync(a => a.Username == trimmedUsername, cancellationToken);

        if (account == null)
        {
            return LoginResultDto.Fail(InvalidCredentialsMessage);
        }

        if (!account.IsActive)
        {
            return LoginResultDto.Fail("Tài khoản đã bị vô hiệu hóa. Vui lòng liên hệ quản trị viên.");
        }

        DateTime now = DateTime.Now;

        if (account.LockedUntil.HasValue && account.LockedUntil.Value > now)
        {
            int remainingMinutes = Math.Max(1, (int)Math.Ceiling((account.LockedUntil.Value - now).TotalMinutes));
            return LoginResultDto.Fail($"Tài khoản đang tạm khóa. Vui lòng thử lại sau {remainingMinutes} phút.");
        }

        if (!VerifyPassword(password, account.PasswordHash))
        {
            account.FailedLoginCount += 1;
            account.UpdatedAt = now;

            if (account.FailedLoginCount >= MaxFailedAttempts)
            {
                account.FailedLoginCount = 0;
                account.LockedUntil = now.AddMinutes(LockoutMinutes);
                await _context.SaveChangesAsync(cancellationToken);

                return LoginResultDto.Fail(
                    $"Sai mật khẩu quá {MaxFailedAttempts} lần. Tài khoản bị khóa {LockoutMinutes} phút.");
            }

            await _context.SaveChangesAsync(cancellationToken);

            int remainingAttempts = MaxFailedAttempts - account.FailedLoginCount;
            return LoginResultDto.Fail(
                $"{InvalidCredentialsMessage} Bạn còn {remainingAttempts} lần thử.");
        }

        account.FailedLoginCount = 0;
        account.LockedUntil = null;
        account.UpdatedAt = now;

        _context.AccessLogs.Add(new AccessLog
        {
            AccountId = account.Id,
            Action = "LOGIN",
            OccurredAt = now,
            IpAddress = ipAddress
        });

        await _context.SaveChangesAsync(cancellationToken);

        return LoginResultDto.Ok(new AuthenticatedUserDto(
            account.Id,
            account.Username,
            ResolveFullName(account),
            account.Role.Code,
            account.Role.Name));
    }

    private static bool VerifyPassword(string password, string passwordHash)
    {
        // Tài khoản seed cũ có hash giả, BCrypt.Verify sẽ ném lỗi thay vì trả false.
        if (string.IsNullOrWhiteSpace(passwordHash) || !passwordHash.StartsWith("$2", StringComparison.Ordinal))
        {
            return false;
        }

        try
        {
            return BCrypt.Net.BCrypt.Verify(password, passwordHash);
        }
        catch (BCrypt.Net.SaltParseException)
        {
            return false;
        }
    }

    private static string ResolveFullName(Account account)
    {
        return account.Doctor?.FullName
               ?? account.Nurse?.FullName
               ?? account.DepartmentHead?.FullName
               ?? account.Patient?.FullName
               ?? account.Username;
    }
}
