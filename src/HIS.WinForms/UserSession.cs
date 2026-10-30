using HIS.BLL.DTOs;
using HIS.BLL.Security;

namespace HIS.WinForms;

public sealed class UserSession
{
    public const string AdminRoleCode = "ADMIN";

    public static UserSession? Current { get; private set; }

    public AuthUserDto? User { get; private set; }

    public bool IsAuthenticated => User != null;

    public string Username => User?.Username ?? string.Empty;

    public string FullName => User?.DisplayName ?? string.Empty;

    public string RoleCode => User?.RoleCode ?? string.Empty;

    public string RoleName => User?.RoleName ?? string.Empty;

    public bool IsAdmin => string.Equals(RoleCode, AdminRoleCode, StringComparison.OrdinalIgnoreCase);

    public UserSession()
    {
        Current = this;
    }

    public void SignIn(AuthUserDto user)
    {
        User = user;
        Current = this;
    }

    public void SignOut()
    {
        User = null;
        if (ReferenceEquals(Current, this))
        {
            Current = null;
        }
    }

    public bool Can(string module) => User?.Permissions.Contains(module) == true;
}
