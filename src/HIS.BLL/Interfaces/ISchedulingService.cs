using HIS.BLL.DTOs;

namespace HIS.BLL.Interfaces;

public interface ISchedulingService
{
    // Sinh slot khám tự động theo lịch làm việc của bác sĩ
    Task<GenerateSlotsResultDto> GenerateSlotsAsync(GenerateSlotsRequestDto request, CancellationToken cancellationToken = default);

    // Tra cứu danh sách slot còn trống theo bộ lọc
    Task<List<AppointmentSlotDto>> GetAvailableSlotsAsync(SlotSearchFilterDto filter, CancellationToken cancellationToken = default);

    // Lấy danh sách slot theo bác sĩ và ngày
    Task<List<AppointmentSlotDto>> GetSlotsByDoctorAndDateAsync(int doctorId, DateOnly date, CancellationToken cancellationToken = default);

    // Lấy danh sách slot theo khoa và ngày
    Task<List<AppointmentSlotDto>> GetSlotsByDepartmentAndDateAsync(int departmentId, DateOnly date, CancellationToken cancellationToken = default);
}
