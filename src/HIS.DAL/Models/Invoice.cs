using System;
using System.Collections.Generic;

namespace HIS.DAL.Models;

public partial class Invoice
{
    public int Id { get; set; }

    public int AppointmentId { get; set; }

    public int PatientId { get; set; }

    public int? InsurancePlanId { get; set; }

    public decimal TotalAmount { get; set; }

    public decimal InsuranceCoveredAmount { get; set; }

    public decimal PatientPaidAmount { get; set; }

    public string Status { get; set; } = null!;

    public string? PaymentMethod { get; set; }

    public int? CashierId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? PaidAt { get; set; }

    public virtual Appointment Appointment { get; set; } = null!;

    public virtual Account? Cashier { get; set; }

    public virtual InsurancePlan? InsurancePlan { get; set; }

    public virtual ICollection<InvoiceItem> InvoiceItems { get; set; } = new List<InvoiceItem>();

    public virtual Patient Patient { get; set; } = null!;
}
