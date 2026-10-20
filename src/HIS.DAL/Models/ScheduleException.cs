using System;
using System.Collections.Generic;

namespace HIS.DAL.Models;

public partial class ScheduleException
{
    public int Id { get; set; }

    public int DoctorId { get; set; }

    public DateOnly Date { get; set; }

    public string? Shift { get; set; }

    public string Type { get; set; } = null!;

    public int? ReplacementDoctorId { get; set; }

    public string? Reason { get; set; }

    public int? ApprovedBy { get; set; }

    public DateTime? ApprovedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Account? ApprovedByNavigation { get; set; }

    public virtual Doctor Doctor { get; set; } = null!;

    public virtual Doctor? ReplacementDoctor { get; set; }
}
