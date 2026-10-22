using System.Globalization;

namespace HIS.Web.Helpers;

/// <summary>Định dạng ngày và tiền theo kiểu Việt Nam (không phụ thuộc culture của máy).</summary>
public static class VietnameseFormat
{
    public static string WeekdayLong(DateOnly d) => d.DayOfWeek switch
    {
        DayOfWeek.Monday => "Thứ Hai",
        DayOfWeek.Tuesday => "Thứ Ba",
        DayOfWeek.Wednesday => "Thứ Tư",
        DayOfWeek.Thursday => "Thứ Năm",
        DayOfWeek.Friday => "Thứ Sáu",
        DayOfWeek.Saturday => "Thứ Bảy",
        _ => "Chủ Nhật"
    };

    public static string WeekdayShort(DateOnly d) => d.DayOfWeek switch
    {
        DayOfWeek.Monday => "Thứ 2",
        DayOfWeek.Tuesday => "Thứ 3",
        DayOfWeek.Wednesday => "Thứ 4",
        DayOfWeek.Thursday => "Thứ 5",
        DayOfWeek.Friday => "Thứ 6",
        DayOfWeek.Saturday => "Thứ 7",
        _ => "CN"
    };

    /// <summary>24/06/2025</summary>
    public static string ShortDate(DateOnly d) => d.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

    /// <summary>Thứ Ba, 24/06/2025</summary>
    public static string FullDate(DateOnly d) => $"{WeekdayLong(d)}, {ShortDate(d)}";

    /// <summary>300.000 đ</summary>
    public static string Money(decimal amount)
        => amount.ToString("#,##0", CultureInfo.InvariantCulture).Replace(',', '.') + " đ";
}