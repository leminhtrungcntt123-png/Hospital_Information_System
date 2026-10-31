using HIS.BLL.DTOs;

namespace HIS.BLL.Interfaces;

public interface IAuthService
{
    Task<LoginResult> LoginAsync(string username, string password, CancellationToken cancellationToken = default);

    Task LogoutAsync(int accountId, CancellationToken cancellationToken = default);
}
