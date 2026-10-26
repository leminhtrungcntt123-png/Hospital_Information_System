using HIS.BLL.DTOs;
using HIS.BLL.Interfaces;
using HIS.DAL;
using HIS.Domain;
using Microsoft.EntityFrameworkCore;

namespace HIS.BLL.Services;

public class DepartmentService : IDepartmentService
{
    private readonly AppDbContext _context;

    public DepartmentService(AppDbContext context)
    {
        _context = context;
    }

    // Lấy danh sách chuyên khoa phòng sắp xếp theo tên
    public async Task<IReadOnlyList<DepartmentListItemDto>> GetDepartmentsAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Departments
            .AsNoTracking()
            .OrderBy(d => d.Name)
            .Select(d => new DepartmentListItemDto(
                d.Id,
                d.Name,
                d.Description ?? ""))
            .ToListAsync(cancellationToken);
    }

    // Thêm mới chuyên khoa phòng
    public async Task<DepartmentListItemDto> CreateDepartmentAsync(string name, string? description, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Tên khoa/phòng không được để trống.", nameof(name));

        var trimmedName = name.Trim();
        if (await _context.Departments.AnyAsync(d => d.Name == trimmedName, cancellationToken))
            throw new InvalidOperationException($"Khoa/phòng '{trimmedName}' đã tồn tại.");

        var dept = new Department
        {
            Name = trimmedName,
            Description = string.IsNullOrWhiteSpace(description) ? null : description.Trim()
        };

        _context.Departments.Add(dept);
        await _context.SaveChangesAsync(cancellationToken);

        return new DepartmentListItemDto(dept.Id, dept.Name, dept.Description ?? "");
    }

    // Cập nhật thông tin chuyên khoa phòng
    public async Task<DepartmentListItemDto> UpdateDepartmentAsync(int id, string name, string? description, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Tên khoa/phòng không được để trống.", nameof(name));

        var dept = await _context.Departments.FirstOrDefaultAsync(d => d.Id == id, cancellationToken)
            ?? throw new InvalidOperationException($"Không tìm thấy khoa/phòng có mã {id}.");

        var trimmedName = name.Trim();
        if (await _context.Departments.AnyAsync(d => d.Id != id && d.Name == trimmedName, cancellationToken))
            throw new InvalidOperationException($"Khoa/phòng '{trimmedName}' đã tồn tại.");

        dept.Name = trimmedName;
        dept.Description = string.IsNullOrWhiteSpace(description) ? null : description.Trim();

        await _context.SaveChangesAsync(cancellationToken);

        return new DepartmentListItemDto(dept.Id, dept.Name, dept.Description ?? "");
    }

    // Xóa chuyên khoa phòng
    public async Task<bool> DeleteDepartmentAsync(int id, CancellationToken cancellationToken = default)
    {
        var dept = await _context.Departments
            .Include(d => d.Doctors)
            .Include(d => d.ClinicRooms)
            .FirstOrDefaultAsync(d => d.Id == id, cancellationToken);

        if (dept == null) return false;

        if (dept.Doctors.Any() || dept.ClinicRooms.Any())
            throw new InvalidOperationException("Không thể xóa khoa đang có bác sĩ hoặc phòng khám liên kết.");

        _context.Departments.Remove(dept);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
