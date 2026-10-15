namespace HIS.Domain;

public class AppointmentStatus
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int SortOrder { get; set; }
    public bool IsTerminal { get; set; } = false;

    // Navigation properties
    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    public ICollection<AppointmentStatusHistory> OldStatusHistories { get; set; } = new List<AppointmentStatusHistory>();
    public ICollection<AppointmentStatusHistory> NewStatusHistories { get; set; } = new List<AppointmentStatusHistory>();
}
