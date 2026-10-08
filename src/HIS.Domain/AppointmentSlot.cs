namespace HIS.Domain;

public class AppointmentSlot
{
    public int Id { get; set; }
    public int DoctorId { get; set; }
    public Doctor Doctor { get; set; } = null!;
    public int RoomId { get; set; }
    public ClinicRoom Room { get; set; } = null!;
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public int Capacity { get; set; } = 1;
    public int BookedCount { get; set; } = 0;
    public string Status { get; set; } = "AVAILABLE";
    public int Version { get; set; } = 0;

    // Navigation properties
    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
