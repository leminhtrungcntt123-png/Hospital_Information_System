using System.Globalization;
using HIS.BLL.DTOs;
using HIS.BLL.Interfaces;
using HIS.DAL;
using HIS.Domain;
using Microsoft.EntityFrameworkCore;

namespace HIS.BLL.Services;

public class BookingService : IBookingService
{
    private readonly AppDbContext _context;

    public BookingService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<BookingDepartmentDto>> GetActiveDepartmentsAsync()
    {
        var departments = await _context.Departments
            .AsNoTracking()
            .OrderBy(d => d.Name)
            .ToListAsync();

        var result = new List<BookingDepartmentDto>();
        foreach (var d in departments)
        {
            result.Add(new BookingDepartmentDto
            {
                Id = d.Id,
                Name = d.Name,
                Description = d.Description,
                SlotsThisWeek = await CountSlotsThisWeekAsync(d.Id)
            });
        }
        return result;
    }

    public async Task<BookingDepartmentDto?> GetDepartmentByIdAsync(int id)
    {
        var d = await _context.Departments
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

        if (d is null) return null;

        return new BookingDepartmentDto
        {
            Id = d.Id,
            Name = d.Name,
            Description = d.Description,
            SlotsThisWeek = await CountSlotsThisWeekAsync(d.Id)
        };
    }

    // Tổng số chỗ còn trống trong 7 ngày khả dụng kế tiếp (dùng cho SlotsThisWeek).
    private async Task<int> CountSlotsThisWeekAsync(int departmentId)
    {
        var from = DateOnly.FromDateTime(DateTime.Today).AddDays(1);
        int total = 0;
        for (int i = 0; i < 7; i++)
        {
            var shifts = await GetShiftsAsync(departmentId, from.AddDays(i));
            total += shifts.Sum(s => s.SlotsLeft);
        }
        return total;
    }

    // 1. Lấy danh sách ca khám chuẩn theo DoctorSchedule & AppointmentSlot
    public async Task<List<BookingShiftDto>> GetShiftsAsync(int departmentId, DateOnly date)
    {
        if (date.DayOfWeek == DayOfWeek.Sunday)
            return new List<BookingShiftDto>();

        int dayOfWeekInt = (int)date.DayOfWeek;

        // Khớp chuẩn DoctorSchedule: Room, DayOfWeek, EffectiveFrom/To, IsActive
        var schedules = await _context.DoctorSchedules
            .AsNoTracking()
            .Include(s => s.Doctor)
            .Include(s => s.Room)
            .Where(s => s.Doctor.DepartmentId == departmentId
                        && s.IsActive
                        && s.EffectiveFrom <= date
                        && (s.EffectiveTo == null || s.EffectiveTo >= date)
                        && s.DayOfWeek == dayOfWeekInt)
            .ToListAsync();

        if (!schedules.Any())
            return new List<BookingShiftDto>();

        var result = new List<BookingShiftDto>();
        var startDateTime = date.ToDateTime(TimeOnly.MinValue);
        var endDateTime = date.ToDateTime(TimeOnly.MaxValue);

        foreach (var schedule in schedules)
        {
            // Khớp chuẩn Appointment: Đếm thông qua Navigation Property a.Slot và a.Status.Code
            int bookedCount = await _context.Appointments
                .AsNoTracking()
                .CountAsync(a => a.Slot != null
                              && a.Slot.DoctorId == schedule.DoctorId
                              && a.Slot.StartTime >= startDateTime
                              && a.Slot.StartTime <= endDateTime
                              && a.Status.Code != "CANCELLED");

            // Tính số capacity dựa trên thời gian ca làm việc / độ dài 1 slot
            int totalMinutes = (int)(schedule.EndTime - schedule.StartTime).TotalMinutes;
            int maxCapacity = schedule.SlotDurationMinutes > 0 ? totalMinutes / schedule.SlotDurationMinutes : 15;

            int slotsLeft = maxCapacity - bookedCount;
            if (slotsLeft < 0) slotsLeft = 0;

            result.Add(new BookingShiftDto
            {
                Key = schedule.Shift,
                Label = schedule.Shift == "MORNING" ? "Ca sáng" : "Ca chiều",
                TimeRange = $"{schedule.StartTime:hh\\:mm} – {schedule.EndTime:hh\\:mm}",
                SlotsLeft = slotsLeft,
                Status = slotsLeft > 0 ? "AVAILABLE" : "FULL",
                DoctorId = schedule.DoctorId,
                DoctorName = schedule.Doctor?.FullName,
                DoctorTitle = schedule.Doctor?.Specialty,
                RoomName = schedule.Room?.RoomName
            });
        }

        return result;
    }

    // 2. Tính trạng thái các ngày trong tuần
    public async Task<List<BookingDayDto>> GetDaysAsync(int departmentId, DateOnly from, int count)
    {
        var days = new List<BookingDayDto>();

        for (int i = 0; i < count; i++)
        {
            var date = from.AddDays(i);
            var shifts = await GetShiftsAsync(departmentId, date);

            int totalSlots = shifts.Sum(s => s.SlotsLeft);

            string state = shifts.Count == 0 ? "off"
                         : totalSlots == 0 ? "full"
                         : totalSlots <= 6 ? "few"
                         : "available";

            days.Add(new BookingDayDto { Date = date, State = state });
        }

        return days;
    }

    public async Task<IEnumerable<Patient>> GetPatientsByAccountIdAsync(int accountId)
    {
        return await _context.Patients
            .AsNoTracking()
            .Where(p => p.AccountId == accountId)
            .ToListAsync();
    }

    // 4. Tạo hồ sơ bệnh nhân mới (quy trình đặt lịch web); lưu ngay để đặt lịch không bị lỗi FK.
    public async Task<Patient> CreatePatientAsync(int accountId, string fullName, DateOnly dateOfBirth, string? gender, string phone, CancellationToken cancellationToken = default)
    {
        var patient = new Patient
        {
            AccountId = accountId,
            FullName = fullName,
            DateOfBirth = dateOfBirth,
            Gender = gender,
            PhoneNumber = phone,
            CreatedAt = DateTime.Now
        };

        _context.Patients.Add(patient);
        await _context.SaveChangesAsync(cancellationToken);
        return patient;
    }

    // 3. Lấy giá khám khớp chuẩn Service ->Type
    public async Task<decimal> GetExaminationFeeAsync(int departmentId)
    {
        var service = await _context.Services
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.IsActive && s.Type == "EXAMINATION");

        return service?.Price ?? 300000m;
    }

    // Tạo lịch hẹn mới: gán trạng thái CONFIRMED từ bảng AppointmentStatus và sinh mã hiển thị.
    public async Task<Appointment> CreateAppointmentAsync(int patientId, int departmentId, DateOnly date, string shiftKey)
    {
        var status = await _context.AppointmentStatuses
            .FirstAsync(s => s.Code == AppointmentStatus.Codes.Confirmed);

        var now = DateTime.Now;
        var appointment = new Appointment
        {
            PatientId = patientId,
            StatusId = status.Id,
            BookingChannel = "WEB",
            VisitType = "SCHEDULED",
            AppointmentCode = $"LH-{date.ToString("yyMMdd", CultureInfo.InvariantCulture)}-{Random.Shared.Next(1000, 10000)}",
            CreatedAt = now,
            UpdatedAt = now
        };

        _context.Appointments.Add(appointment);
        await _context.SaveChangesAsync();
        return appointment;
    }
}