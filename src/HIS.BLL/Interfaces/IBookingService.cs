using HIS.BLL.DTOs;
using HIS.Domain;

namespace HIS.BLL.Interfaces;

public interface IBookingService
{
    // Lấy danh sách chuyên khoa đang hoạt động kèm số chỗ còn trong tuần
    Task<List<BookingDepartmentDto>> GetActiveDepartmentsAsync();

    // Lấy thông tin chi tiết chuyên khoa theo mã định danh (Id)
    Task<BookingDepartmentDto?> GetDepartmentByIdAsync(int id);

    // Lấy trạng thái khả dụng của các ngày trong tuần
    Task<List<BookingDayDto>> GetDaysAsync(int departmentId, DateOnly from, int count);

    // Lấy danh sách ca khám và số chỗ trống theo chuyên khoa và ngày
    Task<List<BookingShiftDto>> GetShiftsAsync(int departmentId, DateOnly date);

    // Lấy danh sách hồ sơ bệnh nhân theo tài khoản
    Task<IEnumerable<Patient>> GetPatientsByAccountIdAsync(int accountId);

    // Tạo mới hồ sơ bệnh nhân
    Task<Patient> CreatePatientAsync(int accountId, string fullName, DateOnly dateOfBirth, string? gender, string phone, CancellationToken cancellationToken = default);

    // Lấy chi phí khám bệnh của chuyên khoa
    Task<decimal> GetExaminationFeeAsync(int departmentId);

    // Tạo mới lịch hẹn khám
    Task<Appointment> CreateAppointmentAsync(int patientId, int departmentId, DateOnly date, string shiftKey);
}
