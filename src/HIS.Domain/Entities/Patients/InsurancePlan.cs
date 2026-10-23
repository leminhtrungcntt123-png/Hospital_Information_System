namespace HIS.Domain;

public class InsurancePlan
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal CoveragePct { get; set; }
    public string? ReferralType { get; set; }

    // Navigation properties
    public ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
}
