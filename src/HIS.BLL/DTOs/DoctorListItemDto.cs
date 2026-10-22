namespace HIS.BLL.DTOs;

public record DoctorListItemDto(
    int Id,
    string FullName,
    string Specialty,
    string DepartmentName,
    string LicenseNumber);