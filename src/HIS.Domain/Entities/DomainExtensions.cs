namespace HIS.Domain;

public static class GenderCodes
{
    public const string Male = "MALE";
    public const string Female = "FEMALE";
    public const string Other = "OTHER";

    public static string ToGenderVietnamese(this string? gender) => gender switch
    {
        Male => "Nam",
        Female => "Nữ",
        Other => "Khác",
        _ => string.Empty
    };
}

public static class BookingChannelCodes
{
    public const string Web = "WEB";
    public const string OnSite = "ON_SITE";

    public static string ToChannelVietnamese(this string? channel) => channel switch
    {
        Web => "Đặt qua web",
        OnSite => "Tại quầy",
        _ => channel ?? string.Empty
    };
}

public static class PriorityLevels
{
    public const int Normal = 0;
    public const int Priority = 1;
    public const int ElderlyOrChild = 2;
    public const int Emergency = 3;

    public static string ToPriorityVietnamese(this int priorityLevel) => priorityLevel switch
    {
        Priority => "Ưu tiên",
        ElderlyOrChild => "Người cao tuổi / trẻ em",
        Emergency => "Cấp cứu",
        _ => "Bình thường"
    };
}

public static class PatientExtensions
{
    // Extension method: Gọi trực tiếp từ bất kỳ số int nào (ví dụ: patient.Id.ToPatientCode())
    public static string ToPatientCode(this int patientId) => $"BN{patientId:D6}";
}