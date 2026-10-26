using HIS.BLL.Interfaces;
using HIS.DAL;
using HIS.Domain;
using Microsoft.EntityFrameworkCore;

namespace HIS.BLL.Services;

public class AppointmentService : IAppointmentService
{
    private readonly AppDbContext _context;

    public AppointmentService(AppDbContext context)
    {
        _context = context;
    }

    // Lấy danh sách tất cả lịch hẹn
    public async Task<IEnumerable<Appointment>> GetAllAppointmentsAsync()
    {
        return await _context.Appointments
            .AsNoTracking()
            .Include(a => a.Patient)
            .Include(a => a.Status)
            .Include(a => a.Slot)
            .OrderByDescending(a => a.Id)
            .ToListAsync();
    }

    // Lấy thông tin chi tiết một lịch hẹn theo mã định danh (Id)
    public async Task<Appointment?> GetAppointmentByIdAsync(int id)
    {
        return await _context.Appointments
            .AsNoTracking()
            .Include(a => a.Patient)
            .Include(a => a.Status)
            .Include(a => a.Slot)
            .FirstOrDefaultAsync(a => a.Id == id);
    }
}
