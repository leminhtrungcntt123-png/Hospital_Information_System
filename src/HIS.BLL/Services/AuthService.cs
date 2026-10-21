using HIS.DAL;
using HIS.Domain;
using Microsoft.EntityFrameworkCore;

namespace HIS.BLL;

public class AuthService : IAuthService
{
    public const int MaxFailedAttempts = 5;
    public static readonly TimeSpan LockDuration = TimeSpan.FromMinutes(5);

    private readonly AppDbContext _context;

    public AuthService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<LoginResult> LoginAsync(string username, string password, CancellationToken cancellationToken = default)
    {
        username = (username ?? string.Empty).Trim();

        if (username.Length == 0 || string.IsNullOrEmpty(password))
        {
            return LoginResult.Fail("Vui lòng nhập tên đăng nhập và mật khẩu.");
        }

        Account? account = await _context.Accounts
            .Include(a => a.Role)
            .Include(a => a.Doctor)
            .Include(a => a.Nurse)
            .Include(a => a.DepartmentHead)
            .Include(a => a.Patient)
            .FirstOrDefaultAsync(a => a.Username == username, cancellationToken);

        if (account == null)
        {
            return LoginResult.Fail("Tên đăng nhập hoặc mật khẩu không đúng.");
        }

        DateTime now = DateTime.Now;

        if (account.LockedUntil is DateTime lockedUntil && lockedUntil > now)
        {
            int minutes = Math.Max(1, (int)Math.Ceiling((lockedUntil - now).TotalMinutes));
            return LoginResult.Fail($"Tài khoản đang tạm khóa do đăng nhập sai nhiều lần. Vui lòng thử lại sau {minutes} phút.");
        }

        if (!account.IsActive)
        {
            return LoginResult.Fail("Tài khoản đã bị vô hiệu hóa. Vui lòng liên hệ quản trị viên.");
        }

        if (!IsBcryptHash(account.PasswordHash))
        {
            return LoginResult.Fail("Tài khoản chưa được thiết lập mật khẩu. Hãy chạy script src/HIS.DAL/Seed/auth_seed.sql.");
        }

        if (!VerifyPassword(password, account.PasswordHash))
        {
            account.FailedLoginCount++;
            account.UpdatedAt = now;
            _context.AccessLogs.Add(new AccessLog
            {
                AccountId = account.Id,
                Action = "LOGIN_FAILED",
                OccurredAt = now
            });

            if (account.FailedLoginCount >= MaxFailedAttempts)
            {
                account.LockedUntil = now.Add(LockDuration);
                account.FailedLoginCount = 0;

                await _context.SaveChangesAsync(cancellationToken);

                return LoginResult.Fail($"Sai mật khẩu {MaxFailedAttempts} lần. Tài khoản bị khóa {LockDuration.TotalMinutes:0} phút.");
            }

            await _context.SaveChangesAsync(cancellationToken);

            int remaining = MaxFailedAttempts - account.FailedLoginCount;
            return LoginResult.Fail($"Tên đăng nhập hoặc mật khẩu không đúng. Còn {remaining} lần thử.");
        }

        if (account.Role.Code == "PATIENT")
        {
            return LoginResult.Fail("Tài khoản bệnh nhân không dùng cho ứng dụng quản trị. Vui lòng đăng nhập tại cổng web.");
        }

        account.FailedLoginCount = 0;
        account.LockedUntil = null;
        account.UpdatedAt = now;

        _context.AccessLogs.Add(new AccessLog
        {
            AccountId = account.Id,
            Action = "LOGIN_SUCCESS",
            OccurredAt = now
        });

        await _context.SaveChangesAsync(cancellationToken);

        return LoginResult.Success(new AuthUserDto(
            account.Id,
            account.Username,
            account.Role.Code,
            account.Role.Name,
            ResolveDisplayName(account),
            RolePermissions.For(account.Role.Code)));
    }

    public async Task LogoutAsync(int accountId, CancellationToken cancellationToken = default)
    {
        _context.AccessLogs.Add(new AccessLog
        {
            AccountId = accountId,
            Action = "LOGOUT",
            OccurredAt = DateTime.Now
        });

        await _context.SaveChangesAsync(cancellationToken);
    }

    private static bool IsBcryptHash(string? hash) =>
        hash is { Length: 60 } && hash.StartsWith("$2", StringComparison.Ordinal);

    private static bool VerifyPassword(string password, string hash)
    {
        try
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }
        catch (BCrypt.Net.SaltParseException)
        {
            return false;
        }
    }

    private static string ResolveDisplayName(Account account) =>
        account.Doctor?.FullName
        ?? account.Nurse?.FullName
        ?? account.DepartmentHead?.FullName
        ?? account.Patient?.FullName
        ?? account.Username;
}
