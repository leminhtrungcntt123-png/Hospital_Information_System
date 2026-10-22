using HIS.BLL.DTOs;
using HIS.Domain;

namespace HIS.BLL.Interfaces;

public interface IBookingService
{
    // Lấy danh sách các chuyên khoa đang hoạt động kèm số chỗ còn trong tuần
    Task<List<BookingDepartmentDto>> GetActiveDepartmentsAsync();

    // Lấy thông tin chi tiết 1 chuyên khoa
    Task<BookingDepartmentDto?> GetDepartmentByIdAsync(int id);

    // 2 hàm thay thế logic tính ca/ngày từ Mock
    Task<List<BookingDayDto>> GetDaysAsync(int departmentId, DateOnly from, int count);
    Task<List<BookingShiftDto>> GetShiftsAsync(int departmentId, DateOnly date);

    // Lấy danh sách bệnh nhân thuộc quản lý của tài khoản đang đăng nhập
    Task<IEnumerable<Patient>> GetPatientsByAccountIdAsync(int accountId);

    // Tạo hồ sơ bệnh nhân mới trong quy trình đặt lịch (lưu xuống CSDL, trả về Id thật)
    Task<Patient> CreatePatientAsync(int accountId, string fullName, DateOnly dateOfBirth, string? gender, string phone, CancellationToken cancellationToken = default);

    // Lấy chi phí khám bệnh của chuyên khoa
    Task<decimal> GetExaminationFeeAsync(int departmentId);

    // Tạo mới lịch hẹn khám (BLL tự gán StatusId, sinh mã lịch hẹn)
    Task<Appointment> CreateAppointmentAsync(int patientId, int departmentId, DateOnly date, string shiftKey);
}
