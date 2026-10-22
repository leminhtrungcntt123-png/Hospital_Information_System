using System.Diagnostics;
using System.Globalization;
using System.Text.RegularExpressions;
using HIS.Domain;
using HIS.Web.Helpers;
using HIS.Web.ViewModels;
using HIS.Web.ViewModels.Booking;
using Microsoft.AspNetCore.Mvc;
using HIS.BLL.Interfaces;
using HIS.BLL.DTOs;
namespace HIS.Web.Controllers;


// Quy trình đặt lịch khám cho bệnh nhân (web).
// Controller chỉ điều hướng và lưu lựa chọn vào Session; dữ liệu lấy từ IBookingData,
// quy tắc nghiệp vụ thật sẽ nằm ở HIS.BLL (BookingService) để WinForms dùng chung.
//
// Các bước: Index -> ChonKhoa -> ChonNgayCa -> ChonNguoiKham -> XacNhan -> ThanhCong
public class BookingController : Controller
{
    private const int DaysToShow = 7;

    // Tạm thờng cố định tài khoản đang đăng nhập; khi có auth sẽ lấy từ Claims/Session.
    private const int CurrentAccountId = 1;

    // Session keys để lưu trạng thái đặt lịch (BookingState) và kết quả đặt lịch thành công (ThanhCongViewModel).
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

    // Chuyển sang dùng dữ liệu thật trong HIS.BLL và tiêm IBookingService vào constructor.
    private readonly IBookingService _bookingService;

    public BookingController(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    // ------------------------------------------------------------ A1: Trang bắt đầu

    public IActionResult Index() => View();

    // ------------------------------------------------------------ Bước 1: Chọn chuyên khoa

    [HttpGet]
    public async Task<IActionResult> ChonKhoa()
    {
        var state = GetState();
        // Nhận DTOs từ BookingService (BLL) rồi map sang ViewModels để trả về View.
        var departments = await _bookingService.GetActiveDepartmentsAsync();

        // Map DTO (BLL) sang ViewModel để trả về View.
        var departmentVms = departments.Select(d => new DepartmentVm
        {
            Id = d.Id,
            Name = d.Name,
            Description = d.Description ?? string.Empty,
            SlotsThisWeek = d.SlotsThisWeek
        }).ToList();

        var selected = departmentVms.FirstOrDefault(d => d.Id == state.DepartmentId && d.SlotsThisWeek > 0);

        return View(new ChonKhoaViewModel
        {
            Departments = departmentVms,
            SelectedDepartmentId = selected?.Id
        });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> ChonKhoa(int departmentId)
    {
        // Nhận DTOs từ BookingService (BLL) rồi map sang ViewModels để kiểm tra số chỗ còn.
        var dept = await _bookingService.GetDepartmentByIdAsync(departmentId);
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
        SaveState(state); // cần tìm hiều khác j savechangeasync

        return RedirectToAction(nameof(ChonNgayCa)); // cần tìm hiều redirecttoaction là hàm gì 
    }

    // ------------------------------------------------------------ Bước 2: Chọn ngày và ca

    [HttpGet]
    public async Task<IActionResult> ChonNgayCa()
    {
        var state = GetState();
        // 2. Kiểm tra DepartmentId trong Session trước để lấy ra biến deptId
        if (state.DepartmentId is not int deptId)
            return RedirectToAction(nameof(ChonKhoa));

        // 3. Sau khi đã có deptId mới truyền vào BLL và lấy DTOs 
        var dept = await _bookingService.GetDepartmentByIdAsync(deptId);
        if (dept is null)
            return RedirectToAction(nameof(ChonKhoa));

        var from = FirstBookableDate();
        // Lấy danh sách ngày khả dụng từ BookingService (BLL) rồi map sang ViewModels để trả về View.
        var days = await _bookingService.GetDaysAsync(dept.Id, from, DaysToShow);
        var dayVms = days.Select(d => new DayVm { Date = d.Date, State = d.State }).ToList();

        var pickable = dayVms.Where(d => d.State != "off").ToList();

        DateOnly selected = state.Date is { } saved && pickable.Any(d => d.Date == saved)
            ? saved
            : (pickable.FirstOrDefault(d => d.State != "full") ?? pickable.FirstOrDefault() ?? dayVms[0]).Date;

        return View(new ChonNgayCaViewModel
        {
            Department = ToDepartmentVm(dept),
            Days = dayVms,
            MonthLabel = $"Tháng {from.ToString("MM/yyyy", CultureInfo.InvariantCulture)}",
            ShiftList = await BuildShiftListAsync(dept.Id, dept.Name, selected, state.Date == selected ? state.Shift : null)
        });
    }

    // Trả về HTML danh sách ca khám của một ngày (booking.js gọi bằng fetch).
    [HttpGet]
    public async Task<IActionResult> CaKham(string? date)
    {
        // kiểm tra deptId 
        var state = GetState();
        if(state.DepartmentId is not int deptId)
            return BadRequest();

        // gọi BLL lấy thông tin khoa
        var dept = await _bookingService.GetDepartmentByIdAsync(deptId);
        if (dept is null)
            return BadRequest();

        // Kiểm tra ngày hợp lệ
        if (!TryParseDate(date, out var d) || !IsBookableDate(d))
            return BadRequest();

        // Gọi BuildShiftListAsync (bất đồng bộ) để lấy danh sách ca từ BLL
        var shiftListVm = await BuildShiftListAsync(dept.Id, dept.Name, d, state.Date == d ? state.Shift : null);

        return PartialView("_ShiftList", shiftListVm);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> ChonNgayCa(string? date, string? shift)
    {
        var state = GetState();
        if (state.DepartmentId is not int deptId)
            return RedirectToAction(nameof(ChonKhoa));

        if (!TryParseDate(date, out var d) || !IsBookableDate(d))
        {
            TempData["Error"] = "Ngày khám không hợp lệ. Vui lòng chọn lại.";
            return RedirectToAction(nameof(ChonNgayCa));
        }

        // Gọi BLL lấy danh sách ca khám thực tế trong DB
        var shifts = await _bookingService.GetShiftsAsync(deptId, d);
        var chosen = shifts.FirstOrDefault(s => s.Key == shift);

        // Kiểm tra ca khám có hợp lệ và còn chỗ trống (Status == "AVAILABLE") không
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
    public async Task<IActionResult> ChonNguoiKham()
    {
        var state = GetState();
        if (Guard(state, needPatient: false) is { } redirect) return redirect;

        // Thêm await để lấy dữ liệu bất đồng bộ từ GetProfilesAsync()
        var profiles = await GetProfilesAsync();

        int? selectedId = profiles.Any(p => p.Id == state.PatientId && p.IsVerified)
        ? state.PatientId
        : profiles.FirstOrDefault(p => p.Relationship == "Bản thân" && p.IsVerified)?.Id;

        return View(new ChonNguoiKhamViewModel { Patients = profiles, SelectedPatientId = selectedId });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> ChonNguoiKham(int patientId)
    {
        var state = GetState();
        if (Guard(state, needPatient: false) is { } redirect) return redirect;

        // Thêm await để lấy dữ liệu bất đồng bộ từ GetProfilesAsync()
        var profiles = await GetProfilesAsync();
        var patient = profiles.FirstOrDefault(p => p.Id == patientId);

        // Kiểm tra hồ sơ người khám có tồn tại và đã được xác minh (IsVerified) không
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

    // Thêm hồ sơ mới ngay trong quy trình đặt lịch; giữ nguyên khoa, ngày, ca đã chọn.
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> ThemHoSo(ThemHoSoRequest request)
    {
        var state = GetState();
        if (Guard(state, needPatient: false) is { } redirect) return redirect;

        // 1. Kiểm tra Form Validation (Model/ViewModel đảm nhận qua Data Annotations)
        if (!ModelState.IsValid)
        {
            var firstError = ModelState.Values
                .SelectMany(v => v.Errors)
                .FirstOrDefault()?.ErrorMessage ?? "Dữ liệu nhập vào không hợp lệ.";

            TempData["Error"] = firstError;
            return RedirectToAction(nameof(ChonNguoiKham));
        }

        // Lấy dữ liệu và dùng null-conditional operator (?) để tránh crash
        var name = request.FullName?.Trim() ?? string.Empty;
        var phone = (request.Phone ?? string.Empty).Replace(" ", "");

        // Ngày sinh phải parse được (ModelState đã ràng buộc định dạng dd/MM/yyyy)
        if (!DateOnly.TryParseExact(request.DateOfBirth?.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dob))
        {
            TempData["Error"] = "Ngày sinh không hợp lệ. Vui lòng nhập lại.";
            return RedirectToAction(nameof(ChonNguoiKham));
        }

        var relationship = request.Relationship is not null && RelationshipLabels.TryGetValue(request.Relationship, out var label)
            ? label
            : RelationshipLabels["OTHER"];

        // 2. Kiểm tra trùng hồ sơ (Database + Session)
        var profiles = await GetProfilesAsync();
        if (profiles.Any(p => p.FullName.Equals(name, StringComparison.OrdinalIgnoreCase) && p.DateOfBirth == dob))
        {
            TempData["Error"] = "Có khả năng trùng hồ sơ: đã có hồ sơ cùng họ tên và ngày sinh. Vui lòng chọn hồ sơ có sẵn.";
            return RedirectToAction(nameof(ChonNguoiKham));
        }

        // 3. Lưu hồ sơ xuống CSDL qua BLL -> có Id thật, đặt lịch ngay được (không lỗi FK)
        var created = await _bookingService.CreatePatientAsync(CurrentAccountId, name, dob, request.Gender, phone);

        // Giữ nhãn quan hệ trong phiên (DB chưa có cột này) để màn chọn ngưới khám hiển thị đúng lựa chọn
        var extras = HttpContext.Session.GetObject<List<PatientVm>>(ExtraProfilesKey) ?? new List<PatientVm>();
        extras.RemoveAll(e => e.Id == created.Id);
        extras.Add(new PatientVm
        {
            Id = created.Id,
            FullName = name,
            Relationship = relationship,
            Code = created.Id.ToPatientCode(),
            DateOfBirth = dob,
            Phone = phone,
            IsVerified = true
        });
        HttpContext.Session.SetObject(ExtraProfilesKey, extras);

        state.PatientId = created.Id;
        SaveState(state);
        return RedirectToAction(nameof(ChonNguoiKham));
    }

    // ------------------------------------------------------------ Bước 4: Xác nhận

    [HttpGet]
    public async Task<IActionResult> XacNhan()
    {
        var state = GetState();
        if (Guard(state, needPatient: true) is { } redirect) return redirect;

        // 1. Lấy thông tin Chuyên khoa từ BLL
        var dept = await _bookingService.GetDepartmentByIdAsync(state.DepartmentId!.Value);
        if (dept is null) return RedirectToAction(nameof(ChonKhoa));

        // 2. Lấy thông tin Bệnh nhân đã chọn
        var profiles = await GetProfilesAsync();
        var patient = profiles.FirstOrDefault(p => p.Id == state.PatientId);
        if (patient is null) return RedirectToAction(nameof(ChonNguoiKham));

        var date = state.Date!.Value;

        // 3. Kiểm tra lại ca khám từ BLL
        var shifts = await _bookingService.GetShiftsAsync(dept.Id, date);
        var shift = shifts.FirstOrDefault(s => s.Key == state.Shift);
        if (shift is null || shift.Status != "AVAILABLE")
        {
            TempData["Error"] = "Ca khám vừa hết chỗ. Vui lòng chọn ca khác, các thông tin còn lại vẫn được giữ.";
            return RedirectToAction(nameof(ChonNgayCa));
        }

        // 4. Lấy chi phí khám từ BLL (thay thế _data.GetExaminationFee)
        var fee = await _bookingService.GetExaminationFeeAsync(dept.Id);

        // 5. Trả về đúng cấu trúc XacNhanViewModel
        return View(new XacNhanViewModel
        {
            Patient = patient,
            Department = ToDepartmentVm(dept),
            Date = date,
            Shift = ToShiftVm(shift),
            Fee = fee
        });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> XacNhan(bool agreed)
    {
        var state = GetState();
        if (Guard(state, needPatient: true) is { } redirect) return redirect;

        // 1. Lấy thông tin Bệnh nhân & Chuyên khoa từ BLL (async)
        var profiles = await GetProfilesAsync();
        var patient = profiles.FirstOrDefault(p => p.Id == state.PatientId);

        var dept = await _bookingService.GetDepartmentByIdAsync(state.DepartmentId!.Value);

        if (patient is null || !patient.IsVerified) return RedirectToAction(nameof(ChonNguoiKham));
        if (dept is null) return RedirectToAction(nameof(ChonKhoa));

        if (!agreed)
        {
            TempData["Error"] = "Vui lòng xác nhận thông tin và đồng ý với quy định đặt lịch khám để tiếp tục.";
            return RedirectToAction(nameof(XacNhan));
        }

        // 2. Kiểm tra lần cuối từ Database: ca khám còn chỗ không?
        var date = state.Date!.Value;
        var shifts = await _bookingService.GetShiftsAsync(dept.Id, date);
        var shift = shifts.FirstOrDefault(s => s.Key == state.Shift);

        if (shift is null || shift.Status != "AVAILABLE")
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

        // 3. Gọi BLL lưu lịch hẹn chính thức xuống Database (BLL gán StatusId, sinh mã)
        var created = await _bookingService.CreateAppointmentAsync(patient.Id, dept.Id, date, shift.Key);
        var shiftVm = ToShiftVm(shift);

        bookedKeys.Add(key);
        HttpContext.Session.SetObject(BookedKeysKey, bookedKeys);
        HttpContext.Session.SetObject(LastBookingKey, new ThanhCongViewModel
        {
            Code = created.AppointmentCode,
            Status = "CONFIRMED",
            Patient = patient,
            DepartmentName = dept.Name,
            Date = date,
            ShiftLabel = shiftVm.Label,
            TimeRange = shiftVm.TimeRange,
            Doctor = shiftVm.Doctor,
            Room = shiftVm.Room
        });

        // 4. Đặt thành công -> Reset trạng thái Session chọn lịch (vẫn giữ PatientId cho lần sau)
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

    // 
    private BookingState GetState()
        => HttpContext.Session.GetObject<BookingState>(StateKey) ?? new BookingState();

    private void SaveState(BookingState state)
        => HttpContext.Session.SetObject(StateKey, state);

    // Hồ sơ có sẵn + hồ sơ người dùng vừa thêm trong phiên này.
    private async Task<List<PatientVm>> GetProfilesAsync()
    {
        // 1. Gọi BLL lấy danh sách bệnh nhân từ Database
        var dbPatients = await _bookingService.GetPatientsByAccountIdAsync(CurrentAccountId);

        // 2. Chuyển đổi (Map) sang PatientVm hiển thị cho UI
        var patientVms = dbPatients.Select(p => new PatientVm
        {
            Id = p.Id,
            FullName = p.FullName,
            Relationship = "Bản thân", // DB chưa có cột quan hệ; phiên có thể phủ nhãn ở bước 3
            Code = p.Id.ToPatientCode(),
            DateOfBirth = p.DateOfBirth,
            Phone = p.PhoneNumber,
            IsVerified = true
        }).ToList();

        // 3. Phủ nhãn quan hệ của hồ sơ vừa thêm trong phiên (đã lưu CSDL, không gộp trùng)
        var extras = HttpContext.Session.GetObject<List<PatientVm>>(ExtraProfilesKey) ?? new List<PatientVm>();
        foreach (var vm in patientVms)
        {
            if (extras.FirstOrDefault(e => e.Id == vm.Id) is { } extra)
                vm.Relationship = extra.Relationship;
        }
        return patientVms;
    }

    // Nếu người dùng nhảy bước khi chưa chọn đủ thì đưa về bước còn thiếu.
    private IActionResult? Guard(BookingState state, bool needPatient)
    {
        if (state.DepartmentId is null) return RedirectToAction(nameof(ChonKhoa));
        if (state.Date is null || string.IsNullOrEmpty(state.Shift)) return RedirectToAction(nameof(ChonNgayCa));
        if (needPatient && state.PatientId is null) return RedirectToAction(nameof(ChonNguoiKham));
        return null;
    }

    private static DepartmentVm ToDepartmentVm(BookingDepartmentDto d) => new()
    {
        Id = d.Id,
        Name = d.Name,
        Description = d.Description ?? string.Empty,
        SlotsThisWeek = d.SlotsThisWeek
    };

    private static ShiftVm ToShiftVm(BookingShiftDto s) => new()
    {
        Key = s.Key,
        Label = s.Label,
        TimeRange = s.TimeRange,
        SlotsLeft = s.SlotsLeft,
        Status = s.Status,
        Doctor = s.DoctorId is { } docId
            ? new DoctorVm
            {
                Id = docId,
                FullName = s.DoctorName ?? string.Empty,
                Title = s.DoctorTitle ?? string.Empty,
                Specialty = s.DoctorTitle ?? string.Empty
            }
            : null,
        Room = s.RoomName ?? string.Empty
    };

    private async Task<ShiftListViewModel> BuildShiftListAsync(int departmentId, string departmentName, DateOnly date, string? selectedShift)
    {
        var shifts = await _bookingService.GetShiftsAsync(departmentId, date);
        var vms = shifts.Select(ToShiftVm).ToList();
        var valid = vms.Any(s => s.Key == selectedShift && s.IsSelectable) ? selectedShift : null;
        return new ShiftListViewModel
        {
            DepartmentName = departmentName,
            Date = date,
            Shifts = vms,
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