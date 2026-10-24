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

public class DashboardOverviewDto
{
    public int TodayPatients { get; set; }
    public int WaitingPatients { get; set; }
    public int CompletedToday { get; set; }
    public int TotalPatients { get; set; }
}

public record DashboardScheduleItemDto(
    string AppointmentCode,
    string PatientName,
    DateTime StartTime,
    string StatusCode,
    string StatusName,
    string DepartmentName);

public record DashboardRoomStatusDto(
    string RoomNumber,
    string DoctorName,
    string Specialty,
    int BookedCount,
    int Capacity);