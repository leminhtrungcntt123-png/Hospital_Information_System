using System;
using System.Collections.Generic;

namespace HIS.DAL.Models;

public partial class Department
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<ClinicRoom> ClinicRooms { get; set; } = new List<ClinicRoom>();

    public virtual DepartmentHead? DepartmentHead { get; set; }

    public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();

    public virtual ICollection<Nurse> Nurses { get; set; } = new List<Nurse>();
}
