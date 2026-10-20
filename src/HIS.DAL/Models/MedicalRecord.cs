using System;
using System.Collections.Generic;

namespace HIS.DAL.Models;

public partial class MedicalRecord
{
    public int Id { get; set; }

    public int AppointmentId { get; set; }

    public int DoctorId { get; set; }

    public string? Symptoms { get; set; }

    public string? ClinicalExamination { get; set; }

    public string? PrimaryDiagnosisCode { get; set; }

    public string? PrimaryDiagnosisDesc { get; set; }

    public string? SecondaryDiagnosis { get; set; }

    public string? Conclusion { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<AccessLog> AccessLogs { get; set; } = new List<AccessLog>();

    public virtual Appointment Appointment { get; set; } = null!;

    public virtual Doctor Doctor { get; set; } = null!;

    public virtual ICollection<LabOrder> LabOrders { get; set; } = new List<LabOrder>();

    public virtual ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
}
