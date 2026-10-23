namespace HIS.Domain;

public class Prescription
{
    public int Id { get; set; }
    public int MedicalRecordId { get; set; }
    public MedicalRecord MedicalRecord { get; set; } = null!;
    public DateOnly PrescribedDate { get; set; }
    public string? Note { get; set; }

    // Navigation properties
    public ICollection<PrescriptionItem> PrescriptionItems { get; set; } = new List<PrescriptionItem>();
}
