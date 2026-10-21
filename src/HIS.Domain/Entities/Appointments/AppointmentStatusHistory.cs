namespace HIS.Domain;

public class AppointmentStatusHistory
{
    public int Id { get; set; }
    public int AppointmentId { get; set; }
    public Appointment Appointment { get; set; } = null!;
    public int? OldStatusId { get; set; }
    public AppointmentStatus? OldStatus { get; set; }
    public int NewStatusId { get; set; }
    public AppointmentStatus NewStatus { get; set; } = null!;
    public int? PerformedBy { get; set; }
    public Account? Performer { get; set; }
    public DateTime ChangedAt { get; set; }
}
