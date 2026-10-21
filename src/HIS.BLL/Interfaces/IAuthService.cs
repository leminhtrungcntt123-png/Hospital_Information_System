namespace HIS.BLL;

public interface IAuthService
{
    Task<LoginResult> LoginAsync(string username, string password, CancellationToken cancellationToken = default);

    Task LogoutAsync(int accountId, CancellationToken cancellationToken = default);
}

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

public sealed record AuthUserDto(
    int AccountId,
    string Username,
    string RoleCode,
    string RoleName,
    string DisplayName,
    IReadOnlyList<string> Permissions);
