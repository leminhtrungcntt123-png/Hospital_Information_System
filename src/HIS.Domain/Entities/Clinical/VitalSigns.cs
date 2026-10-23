namespace HIS.Domain;

public class VitalSigns
{
    public int Id { get; set; }
    public int AppointmentId { get; set; }
    public Appointment Appointment { get; set; } = null!;
    public int? Pulse { get; set; }
    public int? SystolicBp { get; set; }
    public int? DiastolicBp { get; set; }
    public decimal? Temperature { get; set; }
    public int? RespiratoryRate { get; set; }
    public int? Spo2 { get; set; }
    public decimal? Weight { get; set; }
    public decimal? Height { get; set; }
    public int RecordedBy { get; set; }
    public Nurse Recorder { get; set; } = null!;
    public DateTime RecordedAt { get; set; }
}
