using HIS.BLL.DTOs;

namespace HIS.BLL.Session;

public sealed class CurrentUser
{
    public int AccountId { get; private set; }
    public string Username { get; private set; } = string.Empty;
    public string FullName { get; private set; } = string.Empty;
    public string RoleCode { get; private set; } = string.Empty;
    public string RoleName { get; private set; } = string.Empty;
    public DateTime LoginAt { get; private set; }

    public bool IsAuthenticated => AccountId > 0;

    public void SignIn(AuthenticatedUserDto user)
    {
        AccountId = user.AccountId;
        Username = user.Username;
        FullName = user.FullName;
        RoleCode = user.RoleCode;
        RoleName = user.RoleName;
        LoginAt = DateTime.Now;
    }

    public void SignOut()
    {
        AccountId = 0;
        Username = string.Empty;
        FullName = string.Empty;
        RoleCode = string.Empty;
        RoleName = string.Empty;
        LoginAt = default;
    }

    public bool IsInRole(params string[] roles)
    {
        if (!IsAuthenticated)
        {
            return false;
        }

        return roles.Any(role => string.Equals(role, RoleCode, StringComparison.OrdinalIgnoreCase));
    }
}
