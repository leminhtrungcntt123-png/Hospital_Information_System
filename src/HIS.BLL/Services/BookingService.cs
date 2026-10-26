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

        if (departments.Count == 0) return new List<BookingDepartmentDto>();

        var fromDate = DateOnly.FromDateTime(DateTime.Today).AddDays(1);
        var toDate = fromDate.AddDays(6);
        var fromDateTime = fromDate.ToDateTime(TimeOnly.MinValue);
        var toDateTime = toDate.ToDateTime(TimeOnly.MaxValue);

        // Lấy lịch làm việc của bác sĩ trong 7 ngày tới
        var schedules = await _context.DoctorSchedules
            .AsNoTracking()
            .Include(s => s.Doctor)
            .Where(s => s.IsActive
                        && s.EffectiveFrom <= toDate
                        && (s.EffectiveTo == null || s.EffectiveTo >= fromDate))
            .ToListAsync();

        // Thống kê số lượng lịch hẹn đã đặt trong tuần theo bác sĩ và ngày
        var bookedList = await _context.Appointments
            .AsNoTracking()
            .Where(a => a.Slot != null
                        && a.Slot.StartTime >= fromDateTime
                        && a.Slot.StartTime <= toDateTime
                        && a.Status.Code != "CANCELLED")
            .GroupBy(a => new { a.Slot!.DoctorId, Date = DateOnly.FromDateTime(a.Slot.StartTime) })
            .Select(g => new { g.Key.DoctorId, g.Key.Date, Count = g.Count() })
            .ToListAsync();

        var bookedMap = bookedList.ToDictionary(x => (x.DoctorId, x.Date), x => x.Count);

        var deptSlots = departments.ToDictionary(d => d.Id, _ => 0);

        for (int i = 0; i < 7; i++)
        {
            var curDate = fromDate.AddDays(i);
            if (curDate.DayOfWeek == DayOfWeek.Sunday) continue;

            int dayOfWeekInt = (int)curDate.DayOfWeek;
            var daySchedules = schedules.Where(s => s.DayOfWeek == dayOfWeekInt
                                                 && s.EffectiveFrom <= curDate
                                                 && (s.EffectiveTo == null || s.EffectiveTo >= curDate));

            foreach (var schedule in daySchedules)
            {
                if (schedule.Doctor == null) continue;

                int booked = bookedMap.GetValueOrDefault((schedule.DoctorId, curDate), 0);
                int totalMinutes = (int)(schedule.EndTime - schedule.StartTime).TotalMinutes;
                int maxCapacity = schedule.SlotDurationMinutes > 0 ? totalMinutes / schedule.SlotDurationMinutes : 15;
                int slotsLeft = Math.Max(0, maxCapacity - booked);

                if (deptSlots.ContainsKey(schedule.Doctor.DepartmentId))
                {
                    deptSlots[schedule.Doctor.DepartmentId] += slotsLeft;
                }
            }
        }

        return departments.Select(d => new BookingDepartmentDto
        {
            Id = d.Id,
            Name = d.Name,
            Description = d.Description,
            SlotsThisWeek = deptSlots.GetValueOrDefault(d.Id, 0)
        }).ToList();
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

    // Tính tổng số chỗ còn trống trong 7 ngày tới theo chuyên khoa
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

    // Lấy danh sách ca khám và số chỗ trống theo chuyên khoa và ngày
    public async Task<List<BookingShiftDto>> GetShiftsAsync(int departmentId, DateOnly date)
    {
        if (date.DayOfWeek == DayOfWeek.Sunday)
            return new List<BookingShiftDto>();

        int dayOfWeekInt = (int)date.DayOfWeek;

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

        var startDateTime = date.ToDateTime(TimeOnly.MinValue);
        var endDateTime = date.ToDateTime(TimeOnly.MaxValue);
        var doctorIds = schedules.Select(s => s.DoctorId).Distinct().ToList();

        // Đếm số lịch hẹn đã đặt theo từng bác sĩ trong ngày
        var bookedCounts = await _context.Appointments
            .AsNoTracking()
            .Where(a => a.Slot != null
                        && doctorIds.Contains(a.Slot.DoctorId)
                        && a.Slot.StartTime >= startDateTime
                        && a.Slot.StartTime <= endDateTime
                        && a.Status.Code != "CANCELLED")
            .GroupBy(a => a.Slot!.DoctorId)
            .Select(g => new { DoctorId = g.Key, Count = g.Count() })
            .ToDictionaryAsync(g => g.DoctorId, g => g.Count);

        var result = new List<BookingShiftDto>();

        foreach (var schedule in schedules)
        {
            int bookedCount = bookedCounts.GetValueOrDefault(schedule.DoctorId, 0);

            // Tính số lượng chỗ khám tối đa của ca làm việc
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

    // Lấy trạng thái khả dụng của các ngày trong tuần
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

    // Tạo mới hồ sơ bệnh nhân
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

    // Lấy chi phí khám bệnh của chuyên khoa
    public async Task<decimal> GetExaminationFeeAsync(int departmentId)
    {
        var service = await _context.Services
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.IsActive && s.ServiceTypeId == (int)ServiceTypeId.Examination);

        return service?.Price ?? 300000m;
    }

    // Tạo mới lịch hẹn khám
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