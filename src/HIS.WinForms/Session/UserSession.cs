using HIS.BLL;

namespace HIS.WinForms;

public sealed class UserSession
{
    public AuthUserDto? User { get; private set; }

    public bool IsAuthenticated => User != null;

    public void SignIn(AuthUserDto user) => User = user;

    public void SignOut() => User = null;

    public bool Can(string module) => User?.Permissions.Contains(module) == true;
}
