using System;
using System.Collections.Generic;

namespace HIS.DAL.Models;

public partial class VitalSign
{
    public int Id { get; set; }

    public int AppointmentId { get; set; }

    public int? Pulse { get; set; }

    public int? SystolicBp { get; set; }

    public int? DiastolicBp { get; set; }

    public decimal? Temperature { get; set; }

    public int? RespiratoryRate { get; set; }

    public int? Spo2 { get; set; }

    public decimal? Weight { get; set; }

    public decimal? Height { get; set; }

    public int RecordedBy { get; set; }

    public DateTime RecordedAt { get; set; }

    public virtual Appointment Appointment { get; set; } = null!;

    public virtual Nurse RecordedByNavigation { get; set; } = null!;
}
