namespace HIS.Domain;

public class PrescriptionItem
{
    public int Id { get; set; }
    public int PrescriptionId { get; set; }
    public Prescription Prescription { get; set; } = null!;
    public int MedicationId { get; set; }
    public Medication Medication { get; set; } = null!;
    public string? Dosage { get; set; }
    public int Quantity { get; set; }
    public int? DurationDays { get; set; }
    public string? UsageInstructions { get; set; }
}
