using System;
using System.Collections.Generic;

namespace HIS.DAL.Models;

public partial class LabResult
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public string? Result { get; set; }

    public int? PerformedBy { get; set; }

    public DateTime RecordedAt { get; set; }

    public virtual LabOrder Order { get; set; } = null!;

    public virtual Account? PerformedByNavigation { get; set; }
}
