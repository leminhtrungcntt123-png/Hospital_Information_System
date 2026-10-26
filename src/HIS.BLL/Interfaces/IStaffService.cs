using HIS.BLL.DTOs;

namespace HIS.BLL.Interfaces;

public interface IStaffService
{
    // Lấy danh sách tài khoản nhân sự
    Task<IReadOnlyList<StaffListItemDto>> GetStaffAsync(CancellationToken cancellationToken = default);

    // Lấy danh sách vai trò hệ thống
    Task<IReadOnlyList<RoleOptionDto>> GetRolesAsync(CancellationToken cancellationToken = default);

    // Thêm mới tài khoản nhân sự
    Task<StaffListItemDto> CreateStaffAsync(string username, string password, string fullName, int roleId, bool isActive = true, CancellationToken cancellationToken = default);

    // Cập nhật thông tin tài khoản nhân sự
    Task<StaffListItemDto> UpdateStaffAsync(int id, string? password, string fullName, int roleId, bool isActive, CancellationToken cancellationToken = default);

    // Xóa tài khoản nhân sự
    Task<bool> DeleteStaffAsync(int id, CancellationToken cancellationToken = default);

    // Lấy danh sách bác sĩ (ủy quyền cho DoctorService)
    Task<IReadOnlyList<DoctorListItemDto>> GetDoctorsAsync(CancellationToken cancellationToken = default);
}
