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

/// <summary>Phần danh sách ca khám của một ngày. Cũng là model của partial _ShiftList (tải bằng fetch).</summary>
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

    /// <summary>CONFIRMED (đã xác nhận) hoặc PENDING (mới tiếp nhận, chờ xác nhận).</summary>
    public string Status { get; set; } = "CONFIRMED";
    public PatientVm Patient { get; set; } = new();
    public string DepartmentName { get; set; } = string.Empty;
    public DateOnly Date { get; set; }
    public string ShiftLabel { get; set; } = string.Empty;
    public string TimeRange { get; set; } = string.Empty;
    public DoctorVm? Doctor { get; set; }
    public string Room { get; set; } = string.Empty;
}

/// <summary>Dữ liệu form "Thêm hồ sơ người khám".</summary>
public class ThemHoSoRequest
{
    public string? FullName { get; set; }

    /// <summary>Định dạng DD/MM/YYYY.</summary>
    public string? DateOfBirth { get; set; }
    public string? Gender { get; set; }
    public string? Relationship { get; set; }
    public string? Phone { get; set; }
}