using System;
using System.Collections.Generic;

namespace HIS.DAL.Models;

public partial class ClinicRoom
{
    public int Id { get; set; }

    public int DepartmentId { get; set; }

    public string RoomName { get; set; } = null!;

    public string RoomNumber { get; set; } = null!;

    public virtual ICollection<AppointmentSlot> AppointmentSlots { get; set; } = new List<AppointmentSlot>();

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual Department Department { get; set; } = null!;

    public virtual ICollection<DoctorSchedule> DoctorSchedules { get; set; } = new List<DoctorSchedule>();
}
