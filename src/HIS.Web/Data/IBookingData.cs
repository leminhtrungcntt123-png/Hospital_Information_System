using HIS.Web.ViewModels.Booking;

namespace HIS.Web.Data;

/// <summary>
/// Nguồn dữ liệu cho quy trình đặt lịch.
/// Hiện dùng MockBookingData (dữ liệu mẫu). Khi nối database thật, tạo class mới
/// (gọi IBookingService trong HIS.BLL) rồi đổi một dòng đăng ký trong Program.cs.
/// Controller và View không phải sửa.
/// </summary>
public interface IBookingData
{
    /// <summary>Danh sách chuyên khoa (Department) kèm số chỗ còn trong tuần.</summary>
    List<DepartmentVm> GetDepartments();

    DepartmentVm? GetDepartment(int id);

    /// <summary>Trạng thái từng ngày (còn lịch / sắp hết / hết chỗ / nghỉ) của một chuyên khoa.</summary>
    List<DayVm> GetDays(int departmentId, DateOnly from, int count);

    /// <summary>Các ca khám của một ngày, kèm bác sĩ theo lịch trực (DoctorSchedule + AppointmentSlot).</summary>
    List<ShiftVm> GetShifts(int departmentId, DateOnly date);

    /// <summary>Hồ sơ người khám mà tài khoản đang đăng nhập được quản lý (Patient).</summary>
    List<PatientVm> GetPatients();

    /// <summary>Chi phí khám dự kiến (Service.Price, loại EXAMINATION).</summary>
    decimal GetExaminationFee(int departmentId);
}