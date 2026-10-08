namespace HIS.Domain;

public class Account
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public int FailedLoginCount { get; set; } = 0;
    public DateTime? LockedUntil { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    // Navigation properties
    public Patient? Patient { get; set; }
    public Doctor? Doctor { get; set; }
    public Nurse? Nurse { get; set; }
    public DepartmentHead? DepartmentHead { get; set; }
    public ICollection<ScheduleException> ApprovedScheduleExceptions { get; set; } = new List<ScheduleException>();
    public ICollection<AppointmentStatusHistory> PerformedAppointmentHistories { get; set; } = new List<AppointmentStatusHistory>();
    public ICollection<LabResult> PerformedLabResults { get; set; } = new List<LabResult>();
    public ICollection<Invoice> CashierInvoices { get; set; } = new List<Invoice>();
    public ICollection<AccessLog> AccessLogs { get; set; } = new List<AccessLog>();
}
