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
}