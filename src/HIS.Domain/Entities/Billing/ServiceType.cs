namespace HIS.Domain;

public class ServiceType
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;

    // Navigation properties
    public ICollection<Service> Services { get; set; } = new List<Service>();
}
