namespace HIS.Domain;

public class Department
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }

    // Navigation properties
    public ICollection<ClinicRoom> ClinicRooms { get; set; } = new List<ClinicRoom>();
    public ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();
    public ICollection<Nurse> Nurses { get; set; } = new List<Nurse>();
    public DepartmentHead? DepartmentHead { get; set; }
    public ICollection<DutyShift> DutyShifts { get; set; } = new List<DutyShift>();
}
