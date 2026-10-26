using HIS.BLL.DTOs;

namespace HIS.BLL.Interfaces;

public interface IAdminService 
{
    Task<DashboardStatsDto> GetDashboardStatsAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<PatientListItemDto>> GetPatientsAsync(string? search = null, int limit = 100, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<DoctorListItemDto>> GetDoctorsAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<TodayAppointmentItemDto>> GetTodayAppointmentsAsync(CancellationToken cancellationToken = default);
}

