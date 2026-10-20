using System;
using System.Collections.Generic;

namespace HIS.DAL.Models;

public partial class Nurse
{
    public int Id { get; set; }

    public int AccountId { get; set; }

    public int? DepartmentId { get; set; }

    public string FullName { get; set; } = null!;

    public virtual Account Account { get; set; } = null!;

    public virtual Department? Department { get; set; }

    public virtual ICollection<VitalSign> VitalSigns { get; set; } = new List<VitalSign>();
}
