using HIS.BLL.DTOs;
using HIS.BLL.Interfaces;

namespace HIS.BLL.Services;

/// <summary>
/// Dịch vụ tổng hợp quản trị (Facade) điều phối các tác vụ thống kê, bệnh nhân và bác sĩ.
/// </summary>
public class AdminService : IAdminService
{
    private readonly IDashboardService _dashboardService;
    private readonly IPatientService _patientService;
    private readonly IDoctorService _doctorService;

    public AdminService(
        IDashboardService dashboardService,
        IPatientService patientService,
        IDoctorService doctorService)
    {
        _dashboardService = dashboardService;
        _patientService = patientService;
        _doctorService = doctorService;
    }

    // Lấy số liệu thống kê tổng quan cho trang quản trị
    public Task<DashboardStatsDto> GetDashboardStatsAsync(CancellationToken cancellationToken = default)
        => _dashboardService.GetDashboardStatsAsync(cancellationToken);

    // Lấy danh sách bệnh nhân theo từ khóa và giới hạn số lượng
    public Task<IReadOnlyList<PatientListItemDto>> GetPatientsAsync(string? search = null, int limit = 100, CancellationToken cancellationToken = default)
        => _patientService.GetPatientsAsync(search, limit, cancellationToken);

    // Lấy danh sách bác sĩ
    public Task<IReadOnlyList<DoctorListItemDto>> GetDoctorsAsync(CancellationToken cancellationToken = default)
        => _doctorService.GetDoctorsAsync(cancellationToken);

    // Lấy danh sách lịch hẹn trong ngày hôm nay
    public Task<IReadOnlyList<TodayAppointmentItemDto>> GetTodayAppointmentsAsync(CancellationToken cancellationToken = default)
        => _dashboardService.GetTodayAppointmentsAsync(cancellationToken);
}
