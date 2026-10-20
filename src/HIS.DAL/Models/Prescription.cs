using System;
using System.Collections.Generic;

namespace HIS.DAL.Models;

public partial class Prescription
{
    public int Id { get; set; }

    public int MedicalRecordId { get; set; }

    public DateOnly PrescribedDate { get; set; }

    public string? Note { get; set; }

    public virtual MedicalRecord MedicalRecord { get; set; } = null!;

    public virtual ICollection<PrescriptionItem> PrescriptionItems { get; set; } = new List<PrescriptionItem>();
}
