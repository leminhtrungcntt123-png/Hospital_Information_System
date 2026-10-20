using System;
using System.Collections.Generic;

namespace HIS.DAL.Models;

public partial class PrescriptionItem
{
    public int Id { get; set; }

    public int PrescriptionId { get; set; }

    public int MedicationId { get; set; }

    public string? Dosage { get; set; }

    public int Quantity { get; set; }

    public int? DurationDays { get; set; }

    public string? UsageInstructions { get; set; }

    public virtual Medication Medication { get; set; } = null!;

    public virtual Prescription Prescription { get; set; } = null!;
}
