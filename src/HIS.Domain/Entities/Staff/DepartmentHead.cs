namespace HIS.Domain;

public class DepartmentHead
{
    public int Id { get; set; }
    public int AccountId { get; set; }
    public Account Account { get; set; } = null!;
    public int DepartmentId { get; set; }
    public Department Department { get; set; } = null!;
    public string FullName { get; set; } = string.Empty;
}
