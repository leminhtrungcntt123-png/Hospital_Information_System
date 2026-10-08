namespace HIS.Domain;

public class AppointmentStatusHistory
{
    public int Id { get; set; }
    public int AppointmentId { get; set; }
    public Appointment Appointment { get; set; } = null!;
    public string? OldStatus { get; set; }
    public string NewStatus { get; set; } = string.Empty;
    public int? PerformedBy { get; set; }
    public Account? Performer { get; set; }
    public DateTime ChangedAt { get; set; }
}
