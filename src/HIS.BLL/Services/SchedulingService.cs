using HIS.BLL.DTOs;
using HIS.BLL.Interfaces;
using HIS.DAL;
using HIS.Domain;
using Microsoft.EntityFrameworkCore;

namespace HIS.BLL.Services;

public class SchedulingService : ISchedulingService
{
    private readonly AppDbContext _context;

    public SchedulingService(AppDbContext context)
    {
        _context = context;
    }

    // Sinh khung giờ khám tự động theo khoảng ngày
    public async Task<GenerateSlotsResultDto> GenerateSlotsAsync(
        GenerateSlotsRequestDto request, 
        CancellationToken cancellationToken = default)
    {
        if (request.FromDate > request.ToDate)
        {
            return new GenerateSlotsResultDto
            {
                IsSuccess = false,
                Message = "Ngày bắt đầu không được lớn hơn ngày kết thúc."
            };
        }

        var today = DateOnly.FromDateTime(DateTime.Today);
        var fromDate = request.FromDate < today ? today : request.FromDate;

        if (fromDate > request.ToDate)
        {
            return new GenerateSlotsResultDto
            {
                IsSuccess = false,
                Message = "Khoảng thời gian cần sinh slot đã ở trong quá khứ."
            };
        }

        if (request.ToDate.DayNumber - fromDate.DayNumber > 90)
        {
            return new GenerateSlotsResultDto
            {
                IsSuccess = false,
                Message = "Khoảng thời gian sinh slot tối đa là 90 ngày."
            };
        }

        var scheduleQuery = _context.DoctorSchedules
            .Include(s => s.Doctor)
            .Include(s => s.Room)
            .Where(s => s.IsActive
                     && s.EffectiveFrom <= request.ToDate
                     && (s.EffectiveTo == null || s.EffectiveTo >= fromDate));

        if (request.DepartmentId.HasValue)
        {
            scheduleQuery = scheduleQuery.Where(s => s.Doctor.DepartmentId == request.DepartmentId.Value);
        }

        if (request.DoctorId.HasValue)
        {
            scheduleQuery = scheduleQuery.Where(s => s.DoctorId == request.DoctorId.Value);
        }

        var activeSchedules = await scheduleQuery.AsNoTracking().ToListAsync(cancellationToken);
        if (!activeSchedules.Any())
        {
            return new GenerateSlotsResultDto
            {
                IsSuccess = true,
                TotalGenerated = 0,
                TotalDaysProcessed = request.ToDate.DayNumber - fromDate.DayNumber + 1,
                Message = "Không có lịch trực bác sĩ nào trong khoảng thời gian đã chọn."
            };
        }

        var exceptions = await _context.ScheduleExceptions
            .AsNoTracking()
            .Where(e => e.ApprovedBy != null
                     && e.Date >= fromDate
                     && e.Date <= request.ToDate)
            .ToListAsync(cancellationToken);

        var startRange = DateTime.SpecifyKind(fromDate.ToDateTime(TimeOnly.MinValue), DateTimeKind.Unspecified);
        var endRange = DateTime.SpecifyKind(request.ToDate.ToDateTime(TimeOnly.MaxValue), DateTimeKind.Unspecified);

        var existingSlotKeys = await _context.AppointmentSlots
            .AsNoTracking()
            .Where(s => s.StartTime >= startRange && s.StartTime <= endRange)
            .Select(s => new { s.DoctorId, s.StartTime })
            .ToListAsync(cancellationToken);

        var existingSet = new HashSet<(int DoctorId, DateTime StartTime)>(
            existingSlotKeys.Select(k => (k.DoctorId, k.StartTime))
        );

        var newSlots = new List<AppointmentSlot>();
        int skippedExisting = 0;
        int skippedExceptions = 0;
        int daysProcessed = 0;

        for (var curDate = fromDate; curDate <= request.ToDate; curDate = curDate.AddDays(1))
        {
            daysProcessed++;

            int dotNetDow = (int)curDate.DayOfWeek;
            int isoDow = dotNetDow == 0 ? 7 : dotNetDow;

            var daySchedules = activeSchedules.Where(s =>
                s.EffectiveFrom <= curDate &&
                (s.EffectiveTo == null || s.EffectiveTo >= curDate) &&
                (s.DayOfWeek == null || s.DayOfWeek == isoDow || s.DayOfWeek == dotNetDow)
            ).ToList();

            foreach (var schedule in daySchedules)
            {
                var ex = exceptions.FirstOrDefault(e =>
                    e.DoctorId == schedule.DoctorId &&
                    e.Date == curDate &&
                    (string.IsNullOrEmpty(e.Shift) || string.Equals(e.Shift, schedule.Shift, StringComparison.OrdinalIgnoreCase))
                );

                int slotDoctorId = schedule.DoctorId;
                if (ex != null)
                {
                    if (ex.ReplacementDoctorId == null)
                    {
                        skippedExceptions++;
                        continue;
                    }
                    slotDoctorId = ex.ReplacementDoctorId.Value;
                }

                int duration = schedule.SlotDurationMinutes > 0 ? schedule.SlotDurationMinutes : 15;
                var curTime = schedule.StartTime;

                while (curTime.AddMinutes(duration) <= schedule.EndTime)
                {
                    var nextTime = curTime.AddMinutes(duration);
                    var slotStart = DateTime.SpecifyKind(curDate.ToDateTime(curTime), DateTimeKind.Unspecified);
                    var slotEnd = DateTime.SpecifyKind(curDate.ToDateTime(nextTime), DateTimeKind.Unspecified);

                    var key = (slotDoctorId, slotStart);
                    if (existingSet.Contains(key))
                    {
                        skippedExisting++;
                    }
                    else
                    {
                        existingSet.Add(key);
                        newSlots.Add(new AppointmentSlot
                        {
                            DoctorId = slotDoctorId,
                            RoomId = schedule.RoomId,
                            StartTime = slotStart,
                            EndTime = slotEnd,
                            Capacity = 1,
                            BookedCount = 0,
                            Status = "AVAILABLE",
                            Version = 0
                        });
                    }

                    curTime = nextTime;
                }
            }
        }

        if (newSlots.Any())
        {
            await _context.AppointmentSlots.AddRangeAsync(newSlots, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        return new GenerateSlotsResultDto
        {
            IsSuccess = true,
            TotalGenerated = newSlots.Count,
            TotalSkippedExisting = skippedExisting,
            TotalSkippedExceptions = skippedExceptions,
            TotalDaysProcessed = daysProcessed,
            Message = $"Sinh thành công {newSlots.Count:N0} khung giờ khám mới ({skippedExisting:N0} slot đã tồn tại, {skippedExceptions:N0} ca nghỉ)."
        };
    }

    // Tra cứu các khung giờ khám còn trống theo bộ lọc
    public async Task<List<AppointmentSlotDto>> GetAvailableSlotsAsync(
        SlotSearchFilterDto filter, 
        CancellationToken cancellationToken = default)
    {
        var query = _context.AppointmentSlots
            .AsNoTracking()
            .Include(s => s.Doctor)
                .ThenInclude(d => d.Department)
            .Include(s => s.Room)
            .Where(s => s.Status == "AVAILABLE" && s.BookedCount < s.Capacity);

        var dayStart = DateTime.SpecifyKind(filter.Date.ToDateTime(TimeOnly.MinValue), DateTimeKind.Unspecified);
        var dayEnd = DateTime.SpecifyKind(filter.Date.ToDateTime(TimeOnly.MaxValue), DateTimeKind.Unspecified);
        query = query.Where(s => s.StartTime >= dayStart && s.StartTime <= dayEnd);

        var today = DateOnly.FromDateTime(DateTime.Today);
        if (filter.Date == today)
        {
            var now = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
            query = query.Where(s => s.StartTime > now);
        }

        if (filter.DepartmentId.HasValue)
        {
            query = query.Where(s => s.Doctor.DepartmentId == filter.DepartmentId.Value);
        }

        if (filter.DoctorId.HasValue)
        {
            query = query.Where(s => s.DoctorId == filter.DoctorId.Value);
        }

        if (!string.IsNullOrWhiteSpace(filter.Shift))
        {
            var shiftUpper = filter.Shift.Trim().ToUpperInvariant();
            if (shiftUpper == "MORNING")
            {
                var noon = DateTime.SpecifyKind(filter.Date.ToDateTime(new TimeOnly(12, 0)), DateTimeKind.Unspecified);
                query = query.Where(s => s.StartTime < noon);
            }
            else if (shiftUpper == "AFTERNOON")
            {
                var noon = DateTime.SpecifyKind(filter.Date.ToDateTime(new TimeOnly(12, 0)), DateTimeKind.Unspecified);
                var evening = DateTime.SpecifyKind(filter.Date.ToDateTime(new TimeOnly(18, 0)), DateTimeKind.Unspecified);
                query = query.Where(s => s.StartTime >= noon && s.StartTime < evening);
            }
            else if (shiftUpper == "EVENING")
            {
                var evening = DateTime.SpecifyKind(filter.Date.ToDateTime(new TimeOnly(18, 0)), DateTimeKind.Unspecified);
                query = query.Where(s => s.StartTime >= evening);
            }
        }

        var slots = await query
            .OrderBy(s => s.StartTime)
            .ThenBy(s => s.Doctor.FullName)
            .ToListAsync(cancellationToken);

        return slots.Select(MapToDto).ToList();
    }

    // Lấy toàn bộ slot trong ngày của bác sĩ
    public async Task<List<AppointmentSlotDto>> GetSlotsByDoctorAndDateAsync(
        int doctorId, 
        DateOnly date, 
        CancellationToken cancellationToken = default)
    {
        var dayStart = DateTime.SpecifyKind(date.ToDateTime(TimeOnly.MinValue), DateTimeKind.Unspecified);
        var dayEnd = DateTime.SpecifyKind(date.ToDateTime(TimeOnly.MaxValue), DateTimeKind.Unspecified);

        var slots = await _context.AppointmentSlots
            .AsNoTracking()
            .Include(s => s.Doctor)
                .ThenInclude(d => d.Department)
            .Include(s => s.Room)
            .Where(s => s.DoctorId == doctorId && s.StartTime >= dayStart && s.StartTime <= dayEnd)
            .OrderBy(s => s.StartTime)
            .ToListAsync(cancellationToken);

        return slots.Select(MapToDto).ToList();
    }

    // Lấy toàn bộ slot trong ngày của chuyên khoa
    public async Task<List<AppointmentSlotDto>> GetSlotsByDepartmentAndDateAsync(
        int departmentId, 
        DateOnly date, 
        CancellationToken cancellationToken = default)
    {
        var dayStart = DateTime.SpecifyKind(date.ToDateTime(TimeOnly.MinValue), DateTimeKind.Unspecified);
        var dayEnd = DateTime.SpecifyKind(date.ToDateTime(TimeOnly.MaxValue), DateTimeKind.Unspecified);

        var slots = await _context.AppointmentSlots
            .AsNoTracking()
            .Include(s => s.Doctor)
                .ThenInclude(d => d.Department)
            .Include(s => s.Room)
            .Where(s => s.Doctor.DepartmentId == departmentId && s.StartTime >= dayStart && s.StartTime <= dayEnd)
            .OrderBy(s => s.StartTime)
            .ThenBy(s => s.Doctor.FullName)
            .ToListAsync(cancellationToken);

        return slots.Select(MapToDto).ToList();
    }

    private static AppointmentSlotDto MapToDto(AppointmentSlot s)
    {
        return new AppointmentSlotDto
        {
            Id = s.Id,
            DoctorId = s.DoctorId,
            DoctorName = s.Doctor?.FullName ?? $"BS #{s.DoctorId}",
            Specialty = s.Doctor?.Specialty,
            RoomId = s.RoomId,
            RoomName = s.Room?.RoomName ?? $"Phòng #{s.RoomId}",
            RoomNumber = s.Room?.RoomNumber ?? string.Empty,
            DepartmentId = s.Doctor?.DepartmentId ?? 0,
            DepartmentName = s.Doctor?.Department?.Name ?? string.Empty,
            StartTime = s.StartTime,
            EndTime = s.EndTime,
            Capacity = s.Capacity,
            BookedCount = s.BookedCount,
            Status = s.Status
        };
    }
}
