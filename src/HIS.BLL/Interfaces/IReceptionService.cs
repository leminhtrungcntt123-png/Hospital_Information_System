namespace HIS.BLL;

public enum ReceptionTab
{
    Waiting,
    Received,
    Scheduled,
    Cancelled
}

public sealed record ReceptionCountsDto(int Waiting, int Received, int Scheduled, int Cancelled);

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

public sealed record DepartmentOptionDto(int Id, string Name);

public sealed record DoctorOptionDto(int Id, int DepartmentId, string Name, string Specialty);

public sealed class CreateReceptionRequest
{
    public int? PatientId { get; set; }
    public string FullName { get; set; } = string.Empty;
    public DateOnly DateOfBirth { get; set; }
    public string Gender { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string? NationalId { get; set; }
    public string? Address { get; set; }
    public string? InsuranceCardNumber { get; set; }
    public int DepartmentId { get; set; }
    public int? DoctorId { get; set; }
    public int PriorityLevel { get; set; }
    public string? Reason { get; set; }
}

public sealed record CreateReceptionResult(
    int AppointmentId,
    string AppointmentCode,
    int PatientId,
    string PatientCode,
    string QueueNumber,
    bool IsNewPatient);

public sealed record ReceptionActionResult(bool Success, string Message, string QueueNumber = "");

public interface IReceptionService
{
    Task<ReceptionCountsDto> GetCountsAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ReceptionItemDto>> GetReceptionListAsync(ReceptionTab tab, string? search = null, CancellationToken cancellationToken = default);
    Task<ReceptionPatientDto?> FindPatientAsync(string keyword, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<DepartmentOptionDto>> GetDepartmentsAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<DoctorOptionDto>> GetDoctorsAsync(int? departmentId = null, CancellationToken cancellationToken = default);
    Task<CreateReceptionResult> CreateReceptionAsync(CreateReceptionRequest request, CancellationToken cancellationToken = default);
    Task<ReceptionActionResult> CheckInAsync(int appointmentId, CancellationToken cancellationToken = default);
}
