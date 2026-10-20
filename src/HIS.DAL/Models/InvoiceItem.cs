using System;
using System.Collections.Generic;

namespace HIS.DAL.Models;

public partial class InvoiceItem
{
    public int Id { get; set; }

    public int InvoiceId { get; set; }

    public string Category { get; set; } = null!;

    public string Description { get; set; } = null!;

    public decimal Amount { get; set; }

    public virtual Invoice Invoice { get; set; } = null!;
}
