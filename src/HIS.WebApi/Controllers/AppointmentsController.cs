using Microsoft.AspNetCore.Mvc;
using HIS.BLL;
using HIS.BLL.Interfaces;
using HIS.Domain;

namespace HIS.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AppointmentsController : ControllerBase
{
    private readonly IBookingService _bookingService;
    private readonly IAppointmentService _appointmentService;

    // Tiêm cả IAppointmentService và IBookingService vào Controller
    public AppointmentsController(IAppointmentService appointmentService,IBookingService bookingService)
    {
        _appointmentService = appointmentService;
        _bookingService = bookingService;
    }

    [HttpGet("health")]
    public IActionResult HealthCheck()
    {
        return Ok(new { status = "Healthy", timestamp = DateTime.UtcNow });
    }

    // 2. API mới: Lấy danh sách tất cả lịch hẹn từ CSDL PostgreSQL
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Appointment>>> GetAppointments()
    {
        var appointments = await _appointmentService.GetAllAppointmentsAsync();
        return Ok(appointments);
    }

    // 3. API mới: Lấy thông tin chi tiết 1 lịch hẹn theo ID
    [HttpGet("{id}")]
    public async Task<ActionResult<Appointment>> GetAppointment(int id)
    {
        var appointment = await _appointmentService.GetAppointmentByIdAsync(id);
        if (appointment == null)
        {
            return NotFound(new { message = "Không tìm thấy lịch hẹn." });
        }
        return Ok(appointment);
    }
}
