namespace HIS.Domain;

public class LabResult
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public LabOrder Order { get; set; } = null!;
    public string? Result { get; set; }
    public int? PerformedBy { get; set; }
    public Account? Performer { get; set; }
    public DateTime RecordedAt { get; set; }
}
