namespace HIS.Domain;

public class Service
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public decimal Price { get; set; } = 0;
    public bool IsActive { get; set; } = true;

    // Navigation properties
    public ICollection<LabOrder> LabOrders { get; set; } = new List<LabOrder>();


}
