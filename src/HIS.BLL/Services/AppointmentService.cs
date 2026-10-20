using HIS.BLL.Interfaces;
using HIS.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace HIS.BLL;

// Dùng ApplicationDbContext (scaffold từ CSDL thật, HIS.DAL.Models)
// vì interface đang trả về các entity của bộ model này.
public class AppointmentService : IAppointmentService
{
    private readonly ApplicationDbContext _context;

    public AppointmentService(ApplicationDbContext context)
    {
        _context = context;
    }

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
