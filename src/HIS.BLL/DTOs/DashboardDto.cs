namespace HIS.BLL.DTOs;

public class DashboardStatsDto
{
    public int TotalPatients { get; set; }
    public int TotalDoctors { get; set; }
    public int TotalDepartments { get; set; }
    public int TodayAppointments { get; set; }
}

public record TodayAppointmentItemDto(
    string AppointmentCode,
    string PatientName,
    DateTime StartTime,
    string StatusName,
    string BookingChannel);