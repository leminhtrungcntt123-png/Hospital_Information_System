namespace HIS.BLL.DTOs;

// DTO danh sách Khoa phục vụ màn hình Đặt lịch (có thêm SlotsThisWeek)
public class BookingDepartmentDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int SlotsThisWeek { get; set; }
}

public class BookingDayDto
{
    public DateOnly Date { get; set; }
    public string State { get; set; } = "available"; // "off", "full", "few", "available"
}

public class BookingShiftDto
{
    public string Key { get; set; } = string.Empty;     // "MORNING", "AFTERNOON"
    public string Label { get; set; } = string.Empty;   // "Ca sáng", "Ca chiều"
    public string TimeRange { get; set; } = string.Empty; // "07:30 – 11:30"
    public int SlotsLeft { get; set; }
    public string Status { get; set; } = "AVAILABLE";   // "AVAILABLE", "FULL", "NO_SCHEDULE"
    public bool IsSelectable => Status == "AVAILABLE" && SlotsLeft > 0;
    public int? DoctorId { get; set; }
    public string? DoctorName { get; set; }
    public string? DoctorTitle { get; set; }
    public string? RoomName { get; set; }
}