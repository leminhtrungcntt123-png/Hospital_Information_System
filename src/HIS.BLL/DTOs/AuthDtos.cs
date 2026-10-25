namespace HIS.BLL.DTOs;

public sealed class LoginResultDto
{
    public bool Success { get; private init; }
    public string Message { get; private init; } = string.Empty;
    public AuthenticatedUserDto? User { get; private init; }

    public static LoginResultDto Ok(AuthenticatedUserDto user) => new()
    {
        Success = true,
        User = user
    };

    public static LoginResultDto Fail(string message) => new()
    {
        Message = message
    };
}

public sealed record AuthenticatedUserDto(
    int AccountId,
    string Username,
    string FullName,
    string RoleCode,
    string RoleName);
