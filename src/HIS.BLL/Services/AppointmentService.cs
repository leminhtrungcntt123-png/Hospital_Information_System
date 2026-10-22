using HIS.BLL.Interfaces;
using HIS.DAL;             // Thêm dùng AppDbContext
using HIS.Domain;          // Thêm dùng Entity Appointment chuẩn từ Domain
using Microsoft.EntityFrameworkCore;

namespace HIS.BLL;

// Dùng ApplicationDbContext (scaffold từ CSDL thật, HIS.DAL.Models)
// vì interface đang trả về các entity của bộ model này.
public class AppointmentService : IAppointmentService
{
    private readonly AppDbContext _context;

    public AppointmentService(AppDbContext context)
    {
        _context = context;
    }

    // Lấy danh sách tất cả lịch hẹn từ CSDL PostgreSQL
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

    // Lấy thông tin chi tiết 1 lịch hẹn theo ID
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
