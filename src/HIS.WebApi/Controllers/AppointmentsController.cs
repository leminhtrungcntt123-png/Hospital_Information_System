using Microsoft.AspNetCore.Mvc;
using HIS.BLL;

namespace HIS.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AppointmentsController : ControllerBase
{
    private readonly IBookingService _bookingService;

    public AppointmentsController(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    [HttpGet("health")]
    public IActionResult HealthCheck()
    {
        return Ok(new { status = "Healthy", timestamp = DateTime.UtcNow });
    }
}
