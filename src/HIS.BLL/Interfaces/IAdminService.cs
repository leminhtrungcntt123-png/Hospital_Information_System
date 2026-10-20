namespace HIS.BLL;

public interface IAdminService
{
    Task<DashboardStatsDto> GetDashboardStatsAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<PatientListItemDto>> GetPatientsAsync(string? search = null, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<DoctorListItemDto>> GetDoctorsAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<TodayAppointmentItemDto>> GetTodayAppointmentsAsync(CancellationToken cancellationToken = default);
}

public class DashboardStatsDto
{
    public int TotalPatients { get; set; }
    public int TotalDoctors { get; set; }
    public int TotalDepartments { get; set; }
    public int TodayAppointments { get; set; }
}

public record PatientListItemDto(
    int Id,
    string FullName,
    DateOnly DateOfBirth,
    string Gender,
    string PhoneNumber,
    string NationalId,
    string Address,
    string InsuranceCardNumber);

public record DoctorListItemDto(
    int Id,
    string FullName,
    string Specialty,
    string DepartmentName,
    string LicenseNumber);

public record TodayAppointmentItemDto(
    string AppointmentCode,
    string PatientName,
    DateTime StartTime,
    string StatusName,
    string BookingChannel);
