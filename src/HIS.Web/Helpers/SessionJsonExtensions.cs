using System.Text.Json;

namespace HIS.Web.Helpers;

/// <summary>Lưu / đọc object vào Session dưới dạng JSON.</summary>
public static class SessionJsonExtensions
{
    public static void SetObject<T>(this ISession session, string key, T value)
        => session.SetString(key, JsonSerializer.Serialize(value));

    public static T? GetObject<T>(this ISession session, string key)
    {
        var json = session.GetString(key);
        return json is null ? default : JsonSerializer.Deserialize<T>(json);
    }
}