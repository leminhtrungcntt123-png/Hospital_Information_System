namespace HIS.Domain;

public class Invoice
{
    public int Id { get; set; }
    public int AppointmentId { get; set; }
    public Appointment Appointment { get; set; } = null!;
    public int PatientId { get; set; }
    public Patient Patient { get; set; } = null!;
    public int? InsurancePlanId { get; set; }
    public InsurancePlan? InsurancePlan { get; set; }
    public decimal TotalAmount { get; set; } = 0;
    public decimal InsuranceCoveredAmount { get; set; } = 0;
    public decimal PatientPaidAmount { get; set; } = 0;
    public string Status { get; set; } = "UNPAID";
    public string? PaymentMethod { get; set; }
    public int? CashierId { get; set; }
    public Account? Cashier { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? PaidAt { get; set; }

    // Navigation properties
    public ICollection<InvoiceItem> InvoiceItems { get; set; } = new List<InvoiceItem>();
}
