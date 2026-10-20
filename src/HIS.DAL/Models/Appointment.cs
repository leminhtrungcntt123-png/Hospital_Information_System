using System;
using System.Collections.Generic;

namespace HIS.DAL.Models;

public partial class Appointment
{
    public int Id { get; set; }

    public string AppointmentCode { get; set; } = null!;

    public int PatientId { get; set; }

    public int? SlotId { get; set; }

    public int? RoomId { get; set; }

    public int StatusId { get; set; }

    public string BookingChannel { get; set; } = null!;

    public string VisitType { get; set; } = null!;

    public int PriorityLevel { get; set; }

    public string? QueueNumber { get; set; }

    public string? Reason { get; set; }

    public DateTime? HoldUntil { get; set; }

    public int? ParentAppointmentId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<AppointmentStatusHistory> AppointmentStatusHistories { get; set; } = new List<AppointmentStatusHistory>();

    public virtual ICollection<Appointment> InverseParentAppointment { get; set; } = new List<Appointment>();

    public virtual Invoice? Invoice { get; set; }

    public virtual MedicalRecord? MedicalRecord { get; set; }

    public virtual Appointment? ParentAppointment { get; set; }

    public virtual Patient Patient { get; set; } = null!;

    public virtual ClinicRoom? Room { get; set; }

    public virtual AppointmentSlot? Slot { get; set; }

    public virtual AppointmentStatus Status { get; set; } = null!;

    public virtual VitalSign? VitalSign { get; set; }
}
