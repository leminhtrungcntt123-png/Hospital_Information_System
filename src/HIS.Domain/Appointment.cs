namespace HIS.Domain;

public class Appointment
{
    public int Id { get; set; }
    public string AppointmentCode { get; set; } = string.Empty;
    public int PatientId { get; set; }
    public Patient Patient { get; set; } = null!;
    public int? SlotId { get; set; }
    public AppointmentSlot? Slot { get; set; }
    public int? RoomId { get; set; }
    public ClinicRoom? Room { get; set; }
    public string Status { get; set; } = "PENDING";
    public string BookingChannel { get; set; } = "WEB";
    public string VisitType { get; set; } = "SCHEDULED";
    public int PriorityLevel { get; set; } = 0;
    public string? QueueNumber { get; set; }
    public string? Reason { get; set; }
    public DateTime? HoldUntil { get; set; }
    public int? ParentAppointmentId { get; set; }
    public Appointment? ParentAppointment { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    // Navigation properties
    public ICollection<Appointment> ChildAppointments { get; set; } = new List<Appointment>();
    public ICollection<AppointmentStatusHistory> StatusHistories { get; set; } = new List<AppointmentStatusHistory>();
    public VitalSigns? VitalSigns { get; set; }
    public MedicalRecord? MedicalRecord { get; set; }
    public Invoice? Invoice { get; set; }
}
