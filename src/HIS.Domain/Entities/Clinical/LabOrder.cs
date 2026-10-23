namespace HIS.Domain;

public class LabOrder
{
    public int Id { get; set; }
    public int MedicalRecordId { get; set; }
    public MedicalRecord MedicalRecord { get; set; } = null!;
    public int ServiceId { get; set; }
    public Service Service { get; set; } = null!;
    public string? Note { get; set; }
    public string Status { get; set; } = "PENDING";
    public DateTime CreatedAt { get; set; }

    // Navigation properties
    public LabResult? Result { get; set; }
}
