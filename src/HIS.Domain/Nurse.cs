namespace HIS.Domain;

public class Nurse
{
    public int Id { get; set; }
    public int AccountId { get; set; }
    public Account Account { get; set; } = null!;
    public int? DepartmentId { get; set; }
    public Department? Department { get; set; }
    public string FullName { get; set; } = string.Empty;

    // Navigation properties
    public ICollection<VitalSigns> RecordedVitalSigns { get; set; } = new List<VitalSigns>();
}
