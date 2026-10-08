namespace HIS.Domain;

public class AccessLog
{
    public int Id { get; set; }
    public int AccountId { get; set; }
    public Account Account { get; set; } = null!;
    public int? MedicalRecordId { get; set; }
    public MedicalRecord? MedicalRecord { get; set; }
    public string Action { get; set; } = string.Empty;
    public DateTime OccurredAt { get; set; }
    public string? IpAddress { get; set; }
}
