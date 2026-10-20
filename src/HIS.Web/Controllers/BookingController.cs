using System.Diagnostics;
using System.Globalization;
using System.Text.RegularExpressions;
using HIS.Web.Data;
using HIS.Web.Helpers;
using HIS.Web.ViewModels;
using HIS.Web.ViewModels.Booking;
using Microsoft.AspNetCore.Mvc;

namespace HIS.Web.Controllers;

/// <summary>
/// Quy trình đặt lịch khám cho bệnh nhân (web).
/// Controller chỉ điều hướng và lưu lựa chọn vào Session; dữ liệu lấy từ IBookingData,
/// quy tắc nghiệp vụ thật sẽ nằm ở HIS.BLL (BookingService) để WinForms dùng chung.
///
/// Các bước: Index -> ChonKhoa -> ChonNgayCa -> ChonNguoiKham -> XacNhan -> ThanhCong
/// </summary>
public class BookingController : Controller
{
    private const int DaysToShow = 7;

    private const string StateKey = "BookingState";
    private const string LastBookingKey = "LastBooking";
    private const string ExtraProfilesKey = "ExtraProfiles";
    private const string BookedKeysKey = "BookedKeys";

    private static readonly Regex PhoneRegex = new(@"^0\d{9,10}$", RegexOptions.Compiled);

    private static readonly Dictionary<string, string> RelationshipLabels = new()
    {
        ["SELF"] = "Bản thân",
        ["PARENT"] = "Cha/Mẹ",
        ["CHILD"] = "Con",
        ["SPOUSE"] = "Vợ/Chồng",
        ["OTHER"] = "Người thân",
    };

    private static readonly string[] Genders = { "MALE", "FEMALE", "OTHER" };

    private readonly IBookingData _data;

    public BookingController(IBookingData data)
    {
        _data = data;
    }

    // ------------------------------------------------------------ A1: Trang bắt đầu

    public IActionResult Index() => View();

    // ------------------------------------------------------------ Bước 1: Chọn chuyên khoa

    [HttpGet]
    public IActionResult ChonKhoa()
    {
        var state = GetState();
        var departments = _data.GetDepartments();
        var selected = departments.FirstOrDefault(d => d.Id == state.DepartmentId && d.SlotsThisWeek > 0);

        return View(new ChonKhoaViewModel
        {
            Departments = departments,
            SelectedDepartmentId = selected?.Id
        });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult ChonKhoa(int departmentId)
    {
        var dept = _data.GetDepartment(departmentId);
        if (dept is null || dept.SlotsThisWeek == 0)
        {
            TempData["Error"] = "Chuyên khoa này chưa có lịch khả dụng. Vui lòng chọn chuyên khoa khác.";
            return RedirectToAction(nameof(ChonKhoa));
        }

        var state = GetState();
        if (state.DepartmentId != departmentId)
        {
            // Đổi chuyên khoa thì lịch cũ không còn đúng; hồ sơ người khám vẫn giữ lại.
            state.Date = null;
            state.Shift = null;
        }
        state.DepartmentId = departmentId;
        SaveState(state);

        return RedirectToAction(nameof(ChonNgayCa));
    }

    // ------------------------------------------------------------ Bước 2: Chọn ngày và ca

    [HttpGet]
    public IActionResult ChonNgayCa()
    {
        var state = GetState();
        if (state.DepartmentId is not int deptId || _data.GetDepartment(deptId) is not { } dept)
            return RedirectToAction(nameof(ChonKhoa));

        var from = FirstBookableDate();
        var days = _data.GetDays(dept.Id, from, DaysToShow);
        var pickable = days.Where(d => d.State != "off").ToList();

        DateOnly selected = state.Date is { } saved && pickable.Any(d => d.Date == saved)
            ? saved
            : (pickable.FirstOrDefault(d => d.State != "full") ?? pickable.FirstOrDefault() ?? days[0]).Date;

        return View(new ChonNgayCaViewModel
        {
            Department = dept,
            Days = days,
            MonthLabel = $"Tháng {from.ToString("MM/yyyy", CultureInfo.InvariantCulture)}",
            ShiftList = BuildShiftList(dept, selected, state.Date == selected ? state.Shift : null)
        });
    }

    /// <summary>Trả về HTML danh sách ca khám của một ngày (booking.js gọi bằng fetch).</summary>
    [HttpGet]
    public IActionResult CaKham(string? date)
    {
        var state = GetState();
        if (state.DepartmentId is not int deptId || _data.GetDepartment(deptId) is not { } dept)
            return BadRequest();
        if (!TryParseDate(date, out var d) || !IsBookableDate(d))
            return BadRequest();

        return PartialView("_ShiftList", BuildShiftList(dept, d, state.Date == d ? state.Shift : null));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult ChonNgayCa(string? date, string? shift)
    {
        var state = GetState();
        if (state.DepartmentId is not int deptId)
            return RedirectToAction(nameof(ChonKhoa));

        if (!TryParseDate(date, out var d) || !IsBookableDate(d))
        {
            TempData["Error"] = "Ngày khám không hợp lệ. Vui lòng chọn lại.";
            return RedirectToAction(nameof(ChonNgayCa));
        }

        var chosen = _data.GetShifts(deptId, d).FirstOrDefault(s => s.Key == shift);
        if (chosen is null || !chosen.IsSelectable)
        {
            TempData["Error"] = "Ca khám này không còn chỗ trống. Vui lòng chọn ca khác.";
            return RedirectToAction(nameof(ChonNgayCa));
        }

        state.Date = d;
        state.Shift = chosen.Key;
        SaveState(state);

        return RedirectToAction(nameof(ChonNguoiKham));
    }

    // ------------------------------------------------------------ Bước 3: Chọn người khám

    [HttpGet]
    public IActionResult ChonNguoiKham()
    {
        var state = GetState();
        if (Guard(state, needPatient: false) is { } redirect) return redirect;

        var profiles = GetProfiles();
        int? selectedId = profiles.Any(p => p.Id == state.PatientId && p.IsVerified)
            ? state.PatientId
            : profiles.FirstOrDefault(p => p.Relationship == "Bản thân" && p.IsVerified)?.Id;

        return View(new ChonNguoiKhamViewModel { Patients = profiles, SelectedPatientId = selectedId });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult ChonNguoiKham(int patientId)
    {
        var state = GetState();
        if (Guard(state, needPatient: false) is { } redirect) return redirect;

        var patient = GetProfiles().FirstOrDefault(p => p.Id == patientId);
        if (patient is null)
        {
            TempData["Error"] = "Không tìm thấy hồ sơ người khám. Vui lòng chọn lại.";
            return RedirectToAction(nameof(ChonNguoiKham));
        }
        if (!patient.IsVerified)
        {
            TempData["Error"] = "Hồ sơ này cần được bổ sung thông tin trước khi đặt lịch. Vui lòng chọn hồ sơ khác.";
            return RedirectToAction(nameof(ChonNguoiKham));
        }

        state.PatientId = patient.Id;
        SaveState(state);
        return RedirectToAction(nameof(XacNhan));
    }

    /// <summary>Thêm hồ sơ mới ngay trong quy trình đặt lịch; giữ nguyên khoa, ngày, ca đã chọn.</summary>
    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult ThemHoSo(ThemHoSoRequest request)
    {
        var state = GetState();
        if (Guard(state, needPatient: false) is { } redirect) return redirect;

        var name = request.FullName?.Trim() ?? string.Empty;
        var phone = (request.Phone ?? string.Empty).Replace(" ", "");

        if (name.Length == 0 || name.Length > 100)
            return Fail("Vui lòng nhập họ và tên người khám (tối đa 100 ký tự).");
        if (!DateOnly.TryParseExact(request.DateOfBirth?.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture,
                DateTimeStyles.None, out var dob) || dob > DateOnly.FromDateTime(DateTime.Today))
            return Fail("Ngày sinh không hợp lệ. Vui lòng nhập theo định dạng DD/MM/YYYY.");
        if (!PhoneRegex.IsMatch(phone))
            return Fail("Số điện thoại không hợp lệ. Vui lòng nhập 10 đến 11 chữ số, bắt đầu bằng 0.");
        if (!string.IsNullOrEmpty(request.Gender) && !Genders.Contains(request.Gender))
            return Fail("Giới tính không hợp lệ.");

        var relationship = request.Relationship is not null && RelationshipLabels.TryGetValue(request.Relationship, out var label)
            ? label
            : RelationshipLabels["OTHER"];

        var profiles = GetProfiles();
        if (profiles.Any(p => p.FullName.Equals(name, StringComparison.OrdinalIgnoreCase) && p.DateOfBirth == dob))
            return Fail("Có khả năng trùng hồ sơ: đã có hồ sơ cùng họ tên và ngày sinh. Vui lòng chọn hồ sơ có sẵn.");

        var extras = HttpContext.Session.GetObject<List<PatientVm>>(ExtraProfilesKey) ?? new List<PatientVm>();
        var patient = new PatientVm
        {
            Id = 1000 + extras.Count + 1,
            FullName = name,
            Relationship = relationship,
            Code = "Hồ sơ tạm",
            DateOfBirth = dob,
            Phone = phone,
            // MOCK: hồ sơ mới coi như hợp lệ để chạy thử luồng. Thực tế phụ thuộc quy trình xác minh của bệnh viện.
            IsVerified = true
        };
        extras.Add(patient);
        HttpContext.Session.SetObject(ExtraProfilesKey, extras);

        state.PatientId = patient.Id;
        SaveState(state);
        return RedirectToAction(nameof(ChonNguoiKham));

        IActionResult Fail(string message)
        {
            TempData["Error"] = message;
            return RedirectToAction(nameof(ChonNguoiKham));
        }
    }

    // ------------------------------------------------------------ Bước 4: Xác nhận

    [HttpGet]
    public IActionResult XacNhan()
    {
        var state = GetState();
        if (Guard(state, needPatient: true) is { } redirect) return redirect;

        var patient = GetProfiles().FirstOrDefault(p => p.Id == state.PatientId);
        var dept = _data.GetDepartment(state.DepartmentId!.Value);
        if (patient is null || !patient.IsVerified) return RedirectToAction(nameof(ChonNguoiKham));
        if (dept is null) return RedirectToAction(nameof(ChonKhoa));

        var date = state.Date!.Value;
        var shift = _data.GetShifts(dept.Id, date).FirstOrDefault(s => s.Key == state.Shift);
        if (shift is null || !shift.IsSelectable)
        {
            TempData["Error"] = "Ca khám vừa hết chỗ. Vui lòng chọn ca khác, các thông tin còn lại vẫn được giữ.";
            return RedirectToAction(nameof(ChonNgayCa));
        }

        return View(new XacNhanViewModel
        {
            Patient = patient,
            Department = dept,
            Date = date,
            Shift = shift,
            Fee = _data.GetExaminationFee(dept.Id)
        });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult XacNhan(bool agreed)
    {
        var state = GetState();
        if (Guard(state, needPatient: true) is { } redirect) return redirect;

        var patient = GetProfiles().FirstOrDefault(p => p.Id == state.PatientId);
        var dept = _data.GetDepartment(state.DepartmentId!.Value);
        if (patient is null || !patient.IsVerified) return RedirectToAction(nameof(ChonNguoiKham));
        if (dept is null) return RedirectToAction(nameof(ChonKhoa));

        if (!agreed)
        {
            TempData["Error"] = "Vui lòng xác nhận thông tin và đồng ý với quy định đặt lịch khám để tiếp tục.";
            return RedirectToAction(nameof(XacNhan));
        }

        // Kiểm tra lần cuối: ca còn chỗ không?
        var date = state.Date!.Value;
        var shift = _data.GetShifts(dept.Id, date).FirstOrDefault(s => s.Key == state.Shift);
        if (shift is null || !shift.IsSelectable)
        {
            TempData["Error"] = "Ca khám vừa hết chỗ trước khi bạn xác nhận. Vui lòng chọn ca khác, các thông tin còn lại vẫn được giữ.";
            return RedirectToAction(nameof(ChonNgayCa));
        }

        // Kiểm tra trùng lịch: cùng người khám, cùng ngày.
        var bookedKeys = HttpContext.Session.GetObject<List<string>>(BookedKeysKey) ?? new List<string>();
        var key = $"{patient.Id}|{date.ToString("yyyyMMdd", CultureInfo.InvariantCulture)}";
        if (bookedKeys.Contains(key))
        {
            TempData["Error"] = $"{patient.FullName} đã có lịch khám trong ngày {VietnameseFormat.ShortDate(date)}. Vui lòng chọn ngày khác hoặc xem lại lịch hẹn.";
            return RedirectToAction(nameof(XacNhan));
        }

        // MOCK: tạo mã giả. Thực tế BookingService sẽ tạo Appointment, giữ slot (Version),
        // ghi AppointmentStatusHistory và trả về trạng thái PENDING hoặc CONFIRMED.
        var code = $"LH-{date.ToString("yyMMdd", CultureInfo.InvariantCulture)}-{Random.Shared.Next(1000, 10000)}";

        bookedKeys.Add(key);
        HttpContext.Session.SetObject(BookedKeysKey, bookedKeys);
        HttpContext.Session.SetObject(LastBookingKey, new ThanhCongViewModel
        {
            Code = code,
            Status = "CONFIRMED",
            Patient = patient,
            DepartmentName = dept.Name,
            Date = date,
            ShiftLabel = shift.Label,
            TimeRange = shift.TimeRange,
            Doctor = shift.Doctor,
            Room = shift.Room
        });

        // Đặt xong thì xóa lựa chọn để lần đặt sau bắt đầu mới (hồ sơ người khám vẫn nhớ).
        SaveState(new BookingState { PatientId = state.PatientId });

        return RedirectToAction(nameof(ThanhCong));
    }

    // ------------------------------------------------------------ Bước 5: Thành công

    [HttpGet]
    public IActionResult ThanhCong()
    {
        var result = HttpContext.Session.GetObject<ThanhCongViewModel>(LastBookingKey);
        return result is null ? RedirectToAction(nameof(Index)) : View(result);
    }

    // ------------------------------------------------------------ Lỗi

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
        => View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });

    // ------------------------------------------------------------ Helpers

    private BookingState GetState()
        => HttpContext.Session.GetObject<BookingState>(StateKey) ?? new BookingState();

    private void SaveState(BookingState state)
        => HttpContext.Session.SetObject(StateKey, state);

    /// <summary>Hồ sơ có sẵn + hồ sơ người dùng vừa thêm trong phiên này.</summary>
    private List<PatientVm> GetProfiles()
    {
        var extras = HttpContext.Session.GetObject<List<PatientVm>>(ExtraProfilesKey) ?? new List<PatientVm>();
        return _data.GetPatients().Concat(extras).ToList();
    }

    /// <summary>Nếu người dùng nhảy bước khi chưa chọn đủ thì đưa về bước còn thiếu.</summary>
    private IActionResult? Guard(BookingState state, bool needPatient)
    {
        if (state.DepartmentId is null) return RedirectToAction(nameof(ChonKhoa));
        if (state.Date is null || string.IsNullOrEmpty(state.Shift)) return RedirectToAction(nameof(ChonNgayCa));
        if (needPatient && state.PatientId is null) return RedirectToAction(nameof(ChonNguoiKham));
        return null;
    }

    private ShiftListViewModel BuildShiftList(DepartmentVm dept, DateOnly date, string? selectedShift)
    {
        var shifts = _data.GetShifts(dept.Id, date);
        var valid = shifts.Any(s => s.Key == selectedShift && s.IsSelectable) ? selectedShift : null;
        return new ShiftListViewModel
        {
            DepartmentName = dept.Name,
            Date = date,
            Shifts = shifts,
            SelectedShift = valid
        };
    }

    private static DateOnly FirstBookableDate() => DateOnly.FromDateTime(DateTime.Today).AddDays(1);

    private static bool IsBookableDate(DateOnly d)
    {
        var from = FirstBookableDate();
        return d >= from && d < from.AddDays(DaysToShow);
    }

    private static bool TryParseDate(string? text, out DateOnly date)
        => DateOnly.TryParseExact(text, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out date);
}