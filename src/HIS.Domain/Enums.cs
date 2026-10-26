namespace HIS.Domain;

/// <summary>
/// Danh mục vai trò trong hệ thống (khớp với seed roles trong CSDL).
/// </summary>
public enum RoleId
{
    Admin = 1,
    DepartmentHead = 2,
    Doctor = 3,
    Nurse = 4,
    Patient = 5
}

/// <summary>
/// Danh mục phân loại dịch vụ (khớp với seed service_types trong CSDL).
/// </summary>
public enum ServiceTypeId
{
    Examination = 1,
    LabTest = 2,
    Imaging = 3
}

/// <summary>
/// Trạng thái lịch hẹn khám (khớp với seed appointment_statuses trong CSDL).
/// </summary>
public enum AppointmentStatusId
{
    Pending = 1,
    Confirmed = 2,
    CheckedIn = 3,
    ReadyForDoctor = 4,
    InProgress = 5,
    Completed = 6,
    Cancelled = 7,
    NoShow = 8,
    Expired = 9
}
