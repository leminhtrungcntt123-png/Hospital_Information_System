namespace HIS.Domain;

public class Payment
{
    public int Id { get; set; }
    public int InvoiceId { get; set; }
    public Invoice Invoice { get; set; } = null!;
    public decimal Amount { get; set; }
    public string PaymentType { get; set; } = "PAYMENT";
    public string PaymentMethod { get; set; } = "CASH";
    public int? CashierId { get; set; }
    public Account? Cashier { get; set; }
    public string? Note { get; set; }
    public DateTime PaidAt { get; set; }
}
