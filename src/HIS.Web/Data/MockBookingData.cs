using HIS.Web.ViewModels.Booking;

namespace HIS.Web.Data;

/// <summary>
/// DỮ LIỆU MẪU tạm thời, chưa đọc database.
/// Cấu trúc theo schema HIS: Department, Doctor, ClinicRoom, DoctorSchedule (ca MORNING/AFTERNOON), Patient.
/// Số chỗ còn lại được sinh theo ngày một cách cố định (không ngẫu nhiên) để giao diện luôn có đủ các trạng thái:
/// còn lịch, sắp hết, hết chỗ, nghỉ (Chủ nhật), không có lịch trực (Da liễu buổi chiều).
/// </summary>
public class MockBookingData : IBookingData
{
    private const int WeekDays = 7;

    private record DeptDef(int Id, string Name, string Description, string Icon);
    private record Duty(DoctorVm Doctor, string Room);

    private static readonly DeptDef[] DeptDefs =
    {
        new(1, "Tim mạch", "Khám và điều trị các bệnh lý tim mạch", "heart-pulse"),
        new(2, "Nội tổng quát", "Khám sức khỏe và bệnh lý nội khoa", "stethoscope"),
        new(3, "Cơ xương khớp", "Điều trị cơ, xương, khớp và cột sống", "activity"),
        new(4, "Tai Mũi Họng", "Khám tai, mũi, họng cho người lớn", "hospital"),
        new(5, "Nhi khoa", "Chăm sóc sức khỏe trẻ em toàn diện", "user-round"),
        new(6, "Da liễu", "Khám và điều trị bệnh lý về da", "shield-check"),
    };

    // Lịch trực mẫu: (ca sáng, ca chiều). null = không có lịch trực.
    // Tai Mũi Họng không có lịch nào; Da liễu chỉ trực buổi sáng.
    private static readonly Dictionary<int, (Duty? Morning, Duty? Afternoon)> Duties = new()
    {
        [1] = (D(1, "BS.CKI Trần Thu Trang", "Tim mạch", 12, "P. 302 · Tòa A"),
               D(2, "ThS.BS Nguyễn Hoàng Minh", "Tim mạch", 9, "P. 305 · Tòa A")),
        [2] = (D(3, "BS. Lê Hoàng Nam", "Nội tổng quát", 10, "P. 106 · Tòa B"),
               D(4, "BS.CKI Phạm Thanh Hương", "Nội tổng quát", 14, "P. 108 · Tòa B")),
        [3] = (D(5, "BS.CKII Nguyễn Văn Quang", "Cơ xương khớp", 18, "P. 210 · Tòa A"),
               D(6, "ThS.BS Đỗ Minh Châu", "Cơ xương khớp", 8, "P. 212 · Tòa A")),
        [5] = (D(7, "BS.CKI Vũ Thị Lan", "Nhi khoa", 15, "P. 120 · Tòa C"),
               D(8, "BS. Hoàng Đức Anh", "Nhi khoa", 7, "P. 122 · Tòa C")),
        [6] = (D(9, "BS.CKI Bùi Ngọc Mai", "Da liễu", 11, "P. 405 · Tòa A"),
               null),
    };

    private static readonly List<PatientVm> Patients = new()
    {
        new() { Id = 1, FullName = "Nguyễn Minh Anh", Relationship = "Bản thân", Code = "BN-2024-01852",
                DateOfBirth = new DateOnly(1991, 8, 12), Phone = "0912 345 678", IsVerified = true },
        new() { Id = 2, FullName = "Trần Thu Hà", Relationship = "Mẹ", Code = "BN-2023-09241",
                DateOfBirth = new DateOnly(1963, 3, 4), Phone = "0912 345 678", IsVerified = true },
        new() { Id = 3, FullName = "Nguyễn Gia Huy", Relationship = "Con", Code = "Hồ sơ tạm",
                DateOfBirth = new DateOnly(2018, 11, 21), Phone = "0912 345 678", IsVerified = false },
    };

    public List<DepartmentVm> GetDepartments()
    {
        var from = DateOnly.FromDateTime(DateTime.Today).AddDays(1);
        return DeptDefs.Select(d => new DepartmentVm
        {
            Id = d.Id,
            Name = d.Name,
            Description = d.Description,
            Icon = d.Icon,
            SlotsThisWeek = Enumerable.Range(0, WeekDays)
                .Sum(i => GetShifts(d.Id, from.AddDays(i)).Sum(s => s.SlotsLeft))
        }).ToList();
    }

    public DepartmentVm? GetDepartment(int id) => GetDepartments().FirstOrDefault(d => d.Id == id);

    public List<DayVm> GetDays(int departmentId, DateOnly from, int count)
    {
        var days = new List<DayVm>();
        for (int i = 0; i < count; i++)
        {
            var date = from.AddDays(i);
            var shifts = GetShifts(departmentId, date);
            var total = shifts.Sum(s => s.SlotsLeft);
            var state = shifts.Count == 0 ? "off"
                      : total == 0 ? "full"
                      : total <= 6 ? "few"
                      : "available";
            days.Add(new DayVm { Date = date, State = state });
        }
        return days;
    }

    public List<ShiftVm> GetShifts(int departmentId, DateOnly date)
    {
        // Chủ nhật nghỉ, hoặc chuyên khoa chưa có lịch trực -> không có ca nào.
        if (date.DayOfWeek == DayOfWeek.Sunday || !Duties.TryGetValue(departmentId, out var duty))
            return new List<ShiftVm>();

        return new List<ShiftVm>
        {
            BuildShift(departmentId, date, 0, "MORNING", "Ca sáng", "07:30 – 11:30", duty.Morning),
            BuildShift(departmentId, date, 1, "AFTERNOON", "Ca chiều", "13:30 – 16:30", duty.Afternoon),
        };
    }

    public List<PatientVm> GetPatients() => Patients.Select(p => new PatientVm
    {
        Id = p.Id,
        FullName = p.FullName,
        Relationship = p.Relationship,
        Code = p.Code,
        DateOfBirth = p.DateOfBirth,
        Phone = p.Phone,
        IsVerified = p.IsVerified
    }).ToList();

    // TODO: lấy từ bảng services (ServiceType = EXAMINATION). Tạm dùng một mức giá cố định.
    public decimal GetExaminationFee(int departmentId) => 300_000m;

    // ---------------------------------------------------------------- helpers

    private static Duty D(int id, string name, string specialty, int years, string room) => new(
        new DoctorVm
        {
            Id = id,
            FullName = name,
            Title = $"Bác sĩ chuyên khoa {specialty}",
            Specialty = specialty,
            Experience = $"{years} năm kinh nghiệm"
        }, room);

    private static ShiftVm BuildShift(int deptId, DateOnly date, int shiftIndex,
        string key, string label, string timeRange, Duty? duty)
    {
        if (duty is null)
            return new ShiftVm { Key = key, Label = label, TimeRange = timeRange, Status = "NO_SCHEDULE" };

        var left = SlotsLeft(deptId, date, shiftIndex);
        return new ShiftVm
        {
            Key = key,
            Label = label,
            TimeRange = timeRange,
            SlotsLeft = left,
            Status = left > 0 ? "AVAILABLE" : "FULL",
            Doctor = duty.Doctor,
            Room = duty.Room
        };
    }

    /// <summary>Số chỗ còn lại cố định theo (chuyên khoa, ngày, ca).</summary>
    private static int SlotsLeft(int deptId, DateOnly date, int shiftIndex)
    {
        int seed = deptId * 7 + date.DayNumber;
        if (seed % 7 == 3) return 0;                          // ngày hết chỗ
        if (seed % 7 == 5) return 1 + (seed + shiftIndex) % 3; // sắp hết: 1..3 chỗ
        return 4 + (seed + shiftIndex * 3) % 8;                // còn lịch: 4..11 chỗ
    }
}