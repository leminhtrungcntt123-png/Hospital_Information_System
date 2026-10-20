using System;
using System.Collections.Generic;

namespace HIS.DAL.Models;

public partial class Medication
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? ActiveIngredient { get; set; }

    public string? Strength { get; set; }

    public string Unit { get; set; } = null!;

    public decimal Price { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<PrescriptionItem> PrescriptionItems { get; set; } = new List<PrescriptionItem>();
}
