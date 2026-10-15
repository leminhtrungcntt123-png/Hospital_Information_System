namespace HIS.Domain;

public class MedicalRecord
{
    public int Id { get; set; }
    public int AppointmentId { get; set; }
    public Appointment Appointment { get; set; } = null!;
    public int DoctorId { get; set; }
    public Doctor Doctor { get; set; } = null!;
    public string? Symptoms { get; set; }
    public string? ClinicalExamination { get; set; }
    public string? PrimaryDiagnosisCode { get; set; }
    public string? PrimaryDiagnosisDesc { get; set; }
    public string? SecondaryDiagnosis { get; set; }
    public string? Conclusion { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    // Navigation properties
    public ICollection<LabOrder> LabOrders { get; set; } = new List<LabOrder>();
    public ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
    public ICollection<AccessLog> AccessLogs { get; set; } = new List<AccessLog>();
}
