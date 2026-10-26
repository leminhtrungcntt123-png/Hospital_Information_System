using HIS.BLL.DTOs;
using HIS.BLL.Interfaces;
using HIS.DAL;
using HIS.Domain;
using Microsoft.EntityFrameworkCore;

namespace HIS.BLL.Services;

public class StaffService : IStaffService
{
    private readonly AppDbContext _context;
    private readonly IDoctorService _doctorService;

    public StaffService(AppDbContext context, IDoctorService doctorService)
    {
        _context = context;
        _doctorService = doctorService;
    }

    // Lấy danh sách tài khoản nhân sự và vai trò tương ứng trong hệ thống
    public async Task<IReadOnlyList<StaffListItemDto>> GetStaffAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Accounts
            .AsNoTracking()
            .OrderBy(a => a.Role.Name)
            .ThenBy(a => a.Username)
            .Select(a => new StaffListItemDto(
                a.Id,
                a.Username,
                a.Doctor != null ? a.Doctor.FullName
                    : a.Nurse != null ? a.Nurse.FullName
                    : a.DepartmentHead != null ? a.DepartmentHead.FullName
                    : a.Patient != null ? a.Patient.FullName
                    : a.Username,
                a.Role.Name,
                a.Role.Code,
                a.IsActive))
            .ToListAsync(cancellationToken);
    }

    // Lấy danh sách các vai trò trong hệ thống
    public async Task<IReadOnlyList<RoleOptionDto>> GetRolesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Roles
            .AsNoTracking()
            .OrderBy(r => r.Name)
            .Select(r => new RoleOptionDto(r.Id, r.Code, r.Name))
            .ToListAsync(cancellationToken);
    }

    // Thêm mới tài khoản nhân sự
    public async Task<StaffListItemDto> CreateStaffAsync(string username, string password, string fullName, int roleId, bool isActive = true, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(username))
            throw new ArgumentException("Tên đăng nhập không được để trống.", nameof(username));
        if (string.IsNullOrWhiteSpace(password))
            throw new ArgumentException("Mật khẩu không được để trống.", nameof(password));
        if (string.IsNullOrWhiteSpace(fullName))
            throw new ArgumentException("Họ và tên không được để trống.", nameof(fullName));

        var trimmedUsername = username.Trim();
        if (await _context.Accounts.AnyAsync(a => a.Username == trimmedUsername, cancellationToken))
            throw new InvalidOperationException($"Tên đăng nhập '{trimmedUsername}' đã được sử dụng.");

        var role = await _context.Roles.FirstOrDefaultAsync(r => r.Id == roleId, cancellationToken)
            ?? throw new InvalidOperationException("Vai trò được chọn không tồn tại.");

        var now = DateTime.UtcNow;
        var account = new Account
        {
            Username = trimmedUsername,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
            RoleId = roleId,
            IsActive = isActive,
            CreatedAt = now,
            UpdatedAt = now
        };

        _context.Accounts.Add(account);
        await _context.SaveChangesAsync(cancellationToken);

        var trimmedFullName = fullName.Trim();
        if (role.Code == "DOCTOR")
        {
            var firstDept = await _context.Departments.FirstOrDefaultAsync(cancellationToken);
            if (firstDept != null)
            {
                _context.Doctors.Add(new Doctor
                {
                    AccountId = account.Id,
                    FullName = trimmedFullName,
                    DepartmentId = firstDept.Id
                });
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
        else if (role.Code == "NURSE")
        {
            _context.Nurses.Add(new Nurse
            {
                AccountId = account.Id,
                FullName = trimmedFullName
            });
            await _context.SaveChangesAsync(cancellationToken);
        }
        else if (role.Code == "DEPT_HEAD")
        {
            var firstDept = await _context.Departments.FirstOrDefaultAsync(cancellationToken);
            if (firstDept != null)
            {
                _context.DepartmentHeads.Add(new DepartmentHead
                {
                    AccountId = account.Id,
                    FullName = trimmedFullName,
                    DepartmentId = firstDept.Id
                });
                await _context.SaveChangesAsync(cancellationToken);
            }
        }

        return new StaffListItemDto(account.Id, account.Username, trimmedFullName, role.Name, role.Code, account.IsActive);
    }

    // Cập nhật thông tin tài khoản nhân sự
    public async Task<StaffListItemDto> UpdateStaffAsync(int id, string? password, string fullName, int roleId, bool isActive, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(fullName))
            throw new ArgumentException("Họ và tên không được để trống.", nameof(fullName));

        var account = await _context.Accounts
            .Include(a => a.Role)
            .Include(a => a.Doctor)
            .Include(a => a.Nurse)
            .Include(a => a.DepartmentHead)
            .FirstOrDefaultAsync(a => a.Id == id, cancellationToken)
            ?? throw new InvalidOperationException($"Không tìm thấy tài khoản có mã {id}.");

        var role = await _context.Roles.FirstOrDefaultAsync(r => r.Id == roleId, cancellationToken)
            ?? throw new InvalidOperationException("Vai trò được chọn không tồn tại.");

        account.RoleId = roleId;
        account.IsActive = isActive;
        account.UpdatedAt = DateTime.UtcNow;

        if (!string.IsNullOrWhiteSpace(password))
        {
            account.PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
        }

        var trimmedFullName = fullName.Trim();
        if (account.Doctor != null)
            account.Doctor.FullName = trimmedFullName;
        else if (account.Nurse != null)
            account.Nurse.FullName = trimmedFullName;
        else if (account.DepartmentHead != null)
            account.DepartmentHead.FullName = trimmedFullName;
        else if (role.Code == "DOCTOR")
        {
            var firstDept = await _context.Departments.FirstOrDefaultAsync(cancellationToken);
            if (firstDept != null)
                _context.Doctors.Add(new Doctor { AccountId = account.Id, FullName = trimmedFullName, DepartmentId = firstDept.Id });
        }
        else if (role.Code == "NURSE")
        {
            _context.Nurses.Add(new Nurse { AccountId = account.Id, FullName = trimmedFullName });
        }

        await _context.SaveChangesAsync(cancellationToken);

        return new StaffListItemDto(account.Id, account.Username, trimmedFullName, role.Name, role.Code, account.IsActive);
    }

    // Xóa tài khoản nhân sự
    public async Task<bool> DeleteStaffAsync(int id, CancellationToken cancellationToken = default)
    {
        var account = await _context.Accounts
            .Include(a => a.Doctor)
            .Include(a => a.Nurse)
            .Include(a => a.DepartmentHead)
            .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);

        if (account == null) return false;

        if (account.Username.Equals("admin", StringComparison.OrdinalIgnoreCase))
            throw new InvalidOperationException("Không thể xóa tài khoản quản trị viên mặc định (admin).");

        _context.Accounts.Remove(account);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    // Lấy danh sách bác sĩ (ủy quyền cho DoctorService)
    public Task<IReadOnlyList<DoctorListItemDto>> GetDoctorsAsync(CancellationToken cancellationToken = default)
        => _doctorService.GetDoctorsAsync(cancellationToken);
}
