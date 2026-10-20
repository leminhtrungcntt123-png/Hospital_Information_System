using HIS.DAL;
using Microsoft.EntityFrameworkCore;

namespace HIS.BLL;

public class AdminService : IAdminService
{
    private readonly AppDbContext _context;

    public AdminService(AppDbContext context)
    {
        _context = context;
    }

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

    public async Task<IReadOnlyList<PatientListItemDto>> GetPatientsAsync(string? search, CancellationToken cancellationToken = default)
    {
        var query = _context.Patients.AsNoTracking().AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            var keyword = search.Trim();
            query = query.Where(p =>
                EF.Functions.ILike(p.FullName, $"%{keyword}%") ||
                EF.Functions.ILike(p.PhoneNumber, $"%{keyword}%") ||
                (p.NationalId != null && EF.Functions.ILike(p.NationalId, $"%{keyword}%")));
        }

        return await query
            .OrderByDescending(p => p.Id)
            .Select(p => new PatientListItemDto(
                p.Id,
                p.FullName,
                p.DateOfBirth,
                FormatGender(p.Gender),
                p.PhoneNumber,
                p.NationalId ?? "",
                p.Address ?? "",
                p.InsuranceCardNumber ?? ""))
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<DoctorListItemDto>> GetDoctorsAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Doctors
            .AsNoTracking()
            .OrderBy(d => d.FullName)
            .Select(d => new DoctorListItemDto(
                d.Id,
                d.FullName,
                d.Specialty ?? "",
                d.Department.Name,
                d.LicenseNumber ?? ""))
            .ToListAsync(cancellationToken);
    }

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
                FormatBookingChannel(a.BookingChannel)))
            .ToListAsync(cancellationToken);
    }

    private static string FormatGender(string? gender) => gender switch
    {
        "MALE" => "Nam",
        "FEMALE" => "Nữ",
        _ => ""
    };

    private static string FormatBookingChannel(string channel) => channel switch
    {
        "WEB" => "Web",
        "ON_SITE" => "Tại viện",
        _ => channel
    };
}
