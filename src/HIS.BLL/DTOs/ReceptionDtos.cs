namespace HIS.BLL.DTOs;

// --- DTOs Đếm & Thống kê ---
public sealed record ReceptionCountsDto(
    int Waiting,
    int Received,
    int Scheduled,
    int Cancelled);

// --- DTOs Hiển thị Danh sách & Tra cứu ---
public sealed record ReceptionItemDto(
    int AppointmentId,
    string AppointmentCode,
    int PatientId,
    string PatientCode,
    string PatientName,
    string Gender,
    DateOnly DateOfBirth,
    string InsuranceCardNumber,
    string DepartmentName,
    string DoctorName,
    string StatusCode,
    string StatusName,
    DateTime AppointmentTime,
    string BookingChannel,
    string VisitType,
    int PriorityLevel,
    string PriorityLabel,
    string QueueNumber,
    string Reason);

public sealed record ReceptionPatientDto(
    int PatientId,
    string PatientCode,
    string FullName,
    DateOnly DateOfBirth,
    string Gender,
    string PhoneNumber,
    string NationalId,
    string Address,
    string InsuranceCardNumber);

// --- DTOs Thẻ chọn (Dropdown Options) ---
public sealed record DepartmentOptionDto(
    int Id, 
    string Name);

public sealed record DoctorOptionDto(
    int Id, 
    int DepartmentId, 
    string Name, 
    string Specialty);