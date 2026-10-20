using System;
using System.Collections.Generic;

namespace HIS.DAL.Models;

public partial class AppointmentStatusHistory
{
    public int Id { get; set; }

    public int AppointmentId { get; set; }

    public int? OldStatusId { get; set; }

    public int NewStatusId { get; set; }

    public int? PerformedBy { get; set; }

    public DateTime ChangedAt { get; set; }

    public virtual Appointment Appointment { get; set; } = null!;

    public virtual AppointmentStatus NewStatus { get; set; } = null!;

    public virtual AppointmentStatus? OldStatus { get; set; }

    public virtual Account? PerformedByNavigation { get; set; }
}
