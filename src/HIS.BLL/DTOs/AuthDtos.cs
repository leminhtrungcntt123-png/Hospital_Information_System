namespace HIS.BLL.DTOs;

public sealed record AuthUserDto(
    int AccountId,
    string Username,
    string RoleCode,
    string RoleName,
    string DisplayName,
    IReadOnlyList<string> Permissions);

public sealed class LoginResult
{
    public bool Succeeded { get; private init; }

    public string? ErrorMessage { get; private init; }

    public AuthUserDto? User { get; private init; }

    public static LoginResult Success(AuthUserDto user) => new()
    {
        Succeeded = true,
        User = user
    };

    public static LoginResult Fail(string message) => new()
    {
        ErrorMessage = message
    };
}
