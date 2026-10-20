using System;
using System.Collections.Generic;

namespace HIS.DAL.Models;

public partial class Doctor
{
    public int Id { get; set; }

    public int AccountId { get; set; }

    public int DepartmentId { get; set; }

    public string FullName { get; set; } = null!;

    public string? Specialty { get; set; }

    public string? LicenseNumber { get; set; }

    public int AvgConsultationMinutes { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual ICollection<AppointmentSlot> AppointmentSlots { get; set; } = new List<AppointmentSlot>();

    public virtual Department Department { get; set; } = null!;

    public virtual ICollection<DoctorSchedule> DoctorSchedules { get; set; } = new List<DoctorSchedule>();

    public virtual ICollection<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();

    public virtual ICollection<ScheduleException> ScheduleExceptionDoctors { get; set; } = new List<ScheduleException>();

    public virtual ICollection<ScheduleException> ScheduleExceptionReplacementDoctors { get; set; } = new List<ScheduleException>();
}
