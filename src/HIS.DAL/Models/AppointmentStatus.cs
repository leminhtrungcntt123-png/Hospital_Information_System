using System;
using System.Collections.Generic;

namespace HIS.DAL.Models;

public partial class AppointmentStatus
{
    public int Id { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public int SortOrder { get; set; }

    public bool IsTerminal { get; set; }

    public virtual ICollection<AppointmentStatusHistory> AppointmentStatusHistoryNewStatuses { get; set; } = new List<AppointmentStatusHistory>();

    public virtual ICollection<AppointmentStatusHistory> AppointmentStatusHistoryOldStatuses { get; set; } = new List<AppointmentStatusHistory>();

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
