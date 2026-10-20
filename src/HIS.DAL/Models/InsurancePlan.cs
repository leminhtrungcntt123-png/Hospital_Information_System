using System;
using System.Collections.Generic;

namespace HIS.DAL.Models;

public partial class InsurancePlan
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public decimal CoveragePct { get; set; }

    public string? ReferralType { get; set; }

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
}
