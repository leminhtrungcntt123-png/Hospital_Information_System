namespace HIS.BLL.DTOs;

public class GenerateSlotsRequestDto
{
    public DateOnly FromDate { get; set; }
    public DateOnly ToDate { get; set; }
    public int? DepartmentId { get; set; }
    public int? DoctorId { get; set; }
}

public class GenerateSlotsResultDto
{
    public bool IsSuccess { get; set; }
    public int TotalGenerated { get; set; }
    public int TotalSkippedExisting { get; set; }
    public int TotalSkippedExceptions { get; set; }
    public int TotalDaysProcessed { get; set; }
    public string Message { get; set; } = string.Empty;
}

public class SlotSearchFilterDto
{
    public int? DepartmentId { get; set; }
    public int? DoctorId { get; set; }
    public DateOnly Date { get; set; }
    public string? Shift { get; set; }
}

public class AppointmentSlotDto
{
    public int Id { get; set; }
    public int DoctorId { get; set; }
    public string DoctorName { get; set; } = string.Empty;
    public string? Specialty { get; set; }
    public int RoomId { get; set; }
    public string RoomName { get; set; } = string.Empty;
    public string RoomNumber { get; set; } = string.Empty;
    public int DepartmentId { get; set; }
    public string DepartmentName { get; set; } = string.Empty;

    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string TimeRangeText => $"{StartTime:HH:mm} - {EndTime:HH:mm}";

    public int Capacity { get; set; }
    public int BookedCount { get; set; }
    public int AvailableCount => Math.Max(0, Capacity - BookedCount);
    public string Status { get; set; } = "AVAILABLE";

    public bool IsAvailable => Status == "AVAILABLE" && BookedCount < Capacity;
}
