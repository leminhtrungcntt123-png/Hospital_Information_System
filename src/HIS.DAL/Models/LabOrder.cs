using System;
using System.Collections.Generic;

namespace HIS.DAL.Models;

public partial class LabOrder
{
    public int Id { get; set; }

    public int MedicalRecordId { get; set; }

    public int ServiceId { get; set; }

    public string? Note { get; set; }

    public string Status { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual LabResult? LabResult { get; set; }

    public virtual MedicalRecord MedicalRecord { get; set; } = null!;

    public virtual Service Service { get; set; } = null!;
}
