using System;
using System.Collections.Generic;

namespace HIS.DAL.Models;

public partial class DoctorSchedule
{
    public int Id { get; set; }

    public int DoctorId { get; set; }

    public int RoomId { get; set; }

    public int? DayOfWeek { get; set; }

    public string Shift { get; set; } = null!;

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }

    public int SlotDurationMinutes { get; set; }

    public DateOnly EffectiveFrom { get; set; }

    public DateOnly? EffectiveTo { get; set; }

    public bool IsActive { get; set; }

    public virtual Doctor Doctor { get; set; } = null!;

    public virtual ClinicRoom Room { get; set; } = null!;
}
