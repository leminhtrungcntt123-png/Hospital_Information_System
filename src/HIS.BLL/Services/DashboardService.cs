using HIS.DAL;
using HIS.Domain;
using HIS.BLL.DTOs;
using Microsoft.EntityFrameworkCore;

namespace HIS.BLL;

public class DashboardService : IDashboardService
{
    private readonly AppDbContext _context;

    public DashboardService(AppDbContext context)
    {
        _context = context;
    }

    // Lấy các số liệu tổng quan hiển thị trên 4 thẻ Stat Cards
    public async Task<DashboardStatsDto> GetDashboardStatsAsync(CancellationToken cancellationToken = default)
    {
        var today = DateTime.Today;
        var tomorrow = today.AddDays(1);

        return new DashboardStatsDto
        {
            TotalPatients = await _context.Patients.CountAsync(cancellationToken),
            TotalDoctors = await _context.Doctors.CountAsync(cancellationToken),
            TotalDepartments = await _context.Departments.CountAsync(cancellationToken),
            TodayAppointments = await _context.Appointments
                .CountAsync(a => a.Slot != null && a.Slot.StartTime >= today && a.Slot.StartTime < tomorrow, cancellationToken)
        };
    }

    // Lấy danh sách lượt khám trong ngày hôm nay
    public async Task<IReadOnlyList<TodayAppointmentItemDto>> GetTodayAppointmentsAsync(CancellationToken cancellationToken = default)
    {
        var today = DateTime.Today;
        var tomorrow = today.AddDays(1);

        return await _context.Appointments
            .AsNoTracking()
            .Where(a => a.Slot != null && a.Slot.StartTime >= today && a.Slot.StartTime < tomorrow)
            .OrderBy(a => a.Slot!.StartTime)
            .Select(a => new TodayAppointmentItemDto(
                a.AppointmentCode,
                a.Patient.FullName,
                a.Slot!.StartTime,
                a.Status.Name,
                a.BookingChannel.ToChannelVietnamese())) // Dùng Extension Method trong Domain
            .ToListAsync(cancellationToken);
    }

    private static readonly string[] WaitingStatusCodes =
    {
        AppointmentStatus.Codes.Pending,
        AppointmentStatus.Codes.Confirmed
    };

    // Số liệu 4 thẻ trên trang Bàn làm việc
    public async Task<DashboardOverviewDto> GetOverviewAsync(CancellationToken cancellationToken = default)
    {
        DateTime today = DateTime.Today;
        DateTime tomorrow = today.AddDays(1);

        IQueryable<Appointment> todayAppointments = _context.Appointments
            .AsNoTracking()
            .Where(a =>
                (a.Slot != null && a.Slot.StartTime >= today && a.Slot.StartTime < tomorrow) ||
                (a.SlotId == null && a.CreatedAt >= today && a.CreatedAt < tomorrow));

        return new DashboardOverviewDto
        {
            TodayPatients = await todayAppointments
                .Select(a => a.PatientId)
                .Distinct()
                .CountAsync(cancellationToken),
            WaitingPatients = await todayAppointments
                .CountAsync(a => WaitingStatusCodes.Contains(a.Status.Code), cancellationToken),
            CompletedToday = await todayAppointments
                .CountAsync(a => a.Status.Code == AppointmentStatus.Codes.Completed, cancellationToken),
            TotalPatients = await _context.Patients.CountAsync(cancellationToken)
        };
    }

    // Danh sách lịch hẹn hôm nay (có slot) cho bảng chờ ưu tiên và biểu đồ giờ
    public async Task<IReadOnlyList<DashboardScheduleItemDto>> GetTodayScheduleAsync(CancellationToken cancellationToken = default)
    {
        DateTime today = DateTime.Today;
        DateTime tomorrow = today.AddDays(1);

        return await _context.Appointments
            .AsNoTracking()
            .Where(a => a.Slot != null && a.Slot.StartTime >= today && a.Slot.StartTime < tomorrow)
            .OrderBy(a => a.Slot!.StartTime)
            .Select(a => new DashboardScheduleItemDto(
                a.AppointmentCode,
                a.Patient.FullName,
                a.Slot!.StartTime,
                a.Status.Code,
                a.Status.Name,
                a.Slot!.Room.Department.Name))
            .ToListAsync(cancellationToken);
    }

    // Tình trạng các phòng khám có lịch hôm nay (số đã đặt / sức chứa)
    public async Task<IReadOnlyList<DashboardRoomStatusDto>> GetRoomStatusAsync(CancellationToken cancellationToken = default)
    {
        DateTime today = DateTime.Today;
        DateTime tomorrow = today.AddDays(1);

        var slots = await _context.AppointmentSlots
            .AsNoTracking()
            .Where(s => s.StartTime >= today && s.StartTime < tomorrow)
            .Select(s => new
            {
                s.Room.RoomNumber,
                s.Doctor.FullName,
                s.Doctor.Specialty,
                s.BookedCount,
                s.Capacity
            })
            .ToListAsync(cancellationToken);

        return slots
            .GroupBy(s => new { s.RoomNumber, s.FullName, s.Specialty })
            .Select(g => new DashboardRoomStatusDto(
                g.Key.RoomNumber,
                g.Key.FullName,
                g.Key.Specialty ?? string.Empty,
                g.Sum(x => x.BookedCount),
                g.Sum(x => x.Capacity)))
            .OrderBy(r => r.RoomNumber)
            .Take(4)
            .ToList();
    }
}