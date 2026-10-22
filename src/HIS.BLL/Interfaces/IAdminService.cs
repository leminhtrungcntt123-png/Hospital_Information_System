using HIS.BLL.DTOs;

namespace HIS.BLL;

public interface IAdminService 
{
    Task<DashboardStatsDto> GetDashboardStatsAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<PatientListItemDto>> GetPatientsAsync(string? search = null, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<DoctorListItemDto>> GetDoctorsAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<TodayAppointmentItemDto>> GetTodayAppointmentsAsync(CancellationToken cancellationToken = default);
}
