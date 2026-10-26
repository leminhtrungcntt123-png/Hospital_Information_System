namespace HIS.BLL.DTOs;

public record StaffListItemDto(
    int Id,
    string Username,
    string FullName,
    string RoleName,
    string RoleCode,
    bool IsActive)
{
    public string StatusText => IsActive ? "Đang hoạt động" : "Đã khóa";
}

public record RoleOptionDto(int Id, string Code, string Name);

public enum StaffStatusFilter
{
    All = 0,
    Active = 1,
    Inactive = 2,
    Locked = 3
}

public enum StaffRoleFilter
{
    All = 0,
    Admin = 1,
    Doctor = 2,
    Nurse = 3,
    Receptionist = 4,
    Other = 5
}
