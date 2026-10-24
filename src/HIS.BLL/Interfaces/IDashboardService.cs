using HIS.BLL.DTOs;

namespace HIS.BLL;

public interface IDashboardService
{
    Task<DashboardStatsDto> GetDashboardStatsAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<TodayAppointmentItemDto>> GetTodayAppointmentsAsync(CancellationToken cancellationToken = default);
    Task<DashboardOverviewDto> GetOverviewAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<DashboardScheduleItemDto>> GetTodayScheduleAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<DashboardRoomStatusDto>> GetRoomStatusAsync(CancellationToken cancellationToken = default);
}