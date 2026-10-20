using System;
using System.Collections.Generic;

namespace HIS.DAL.Models;

public partial class AccessLog
{
    public int Id { get; set; }

    public int AccountId { get; set; }

    public int? MedicalRecordId { get; set; }

    public string Action { get; set; } = null!;

    public DateTime OccurredAt { get; set; }

    public string? IpAddress { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual MedicalRecord? MedicalRecord { get; set; }
}
