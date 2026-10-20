using System;
using System.Collections.Generic;

namespace HIS.DAL.Models;

public partial class AppointmentSlot
{
    public int Id { get; set; }

    public int DoctorId { get; set; }

    public int RoomId { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public int Capacity { get; set; }

    public int BookedCount { get; set; }

    public string Status { get; set; } = null!;

    public int Version { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual Doctor Doctor { get; set; } = null!;

    public virtual ClinicRoom Room { get; set; } = null!;
}
