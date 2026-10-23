using System.ComponentModel.DataAnnotations;

namespace HIS.Web.ViewModels.Booking;

// ViewModel của từng màn hình đặt lịch (chỉ phục vụ hiển thị ở HIS.Web).

public class ChonKhoaViewModel
{
    public List<DepartmentVm> Departments { get; set; } = new();
    public int? SelectedDepartmentId { get; set; }
}

public class ChonNgayCaViewModel
{
    public DepartmentVm Department { get; set; } = new();
    public List<DayVm> Days { get; set; } = new();
    public string MonthLabel { get; set; } = string.Empty;
    public ShiftListViewModel ShiftList { get; set; } = new();
}

// Phần danh sách ca khám của một ngày. Cũng là model của partial _ShiftList (tải bằng fetch).</summary>
public class ShiftListViewModel
{
    public string DepartmentName { get; set; } = string.Empty;
    public DateOnly Date { get; set; }
    public List<ShiftVm> Shifts { get; set; } = new();
    public string? SelectedShift { get; set; }
}

public class ChonNguoiKhamViewModel
{
    public List<PatientVm> Patients { get; set; } = new();
    public int? SelectedPatientId { get; set; }
}

public class XacNhanViewModel
{
    public PatientVm Patient { get; set; } = new();
    public DepartmentVm Department { get; set; } = new();
    public DateOnly Date { get; set; }
    public ShiftVm Shift { get; set; } = new();
    public decimal Fee { get; set; }
}

public class ThanhCongViewModel
{
    public string Code { get; set; } = string.Empty;

    // CONFIRMED (đã xác nhận) hoặc PENDING (mới tiếp nhận, chờ xác nhận).
    public string Status { get; set; } = "CONFIRMED";
    public PatientVm Patient { get; set; } = new();
    public string DepartmentName { get; set; } = string.Empty;
    public DateOnly Date { get; set; }
    public string ShiftLabel { get; set; } = string.Empty;
    public string TimeRange { get; set; } = string.Empty;
    public DoctorVm? Doctor { get; set; }
    public string Room { get; set; } = string.Empty;
}

// Dữ liệu form "Thêm hồ sơ người khám".
public class ThemHoSoRequest
{
    [Required(ErrorMessage = "Vui lòng nhập họ và tên.")]
    [StringLength(100, ErrorMessage = "Họ tên không được vượt quá 100 ký tự.")]
    public string? FullName { get; set; }

    // Định dạng DD/MM/YYYY.
    [Required(ErrorMessage = "Vui lòng nhập ngày sinh.")]
    [RegularExpression(@"^\d{2}/\d{2}/\d{4}$", ErrorMessage = "Ngày sinh phải theo định dạng DD/MM/YYYY.")]
    public string? DateOfBirth { get; set; }

    public string? Gender { get; set; }

    public string? Relationship { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập số điện thoại.")]
    [RegularExpression(@"^0\d{9,10}$", ErrorMessage = "Số điện thoại phải từ 10-11 số và bắt đầu bằng 0.")]
    public string? Phone { get; set; }
}