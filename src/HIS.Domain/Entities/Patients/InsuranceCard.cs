namespace HIS.Domain;

public class InsuranceCard
{
    public int Id { get; set; }
    public int PatientId { get; set; }
    public Patient Patient { get; set; } = null!;
    public string CardNumber { get; set; } = string.Empty;
    public int InsurancePlanId { get; set; }
    public InsurancePlan InsurancePlan { get; set; } = null!;
    public string? RegisteredFacility { get; set; }
    public bool IsRegisteredHere { get; set; } = false;
    public DateOnly ValidFrom { get; set; }
    public DateOnly ValidTo { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; }

    // Navigation properties
    public ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
}
