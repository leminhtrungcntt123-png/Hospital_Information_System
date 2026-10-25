using HIS.BLL.DTOs;

namespace HIS.BLL;

public interface IAuthService
{
    Task<LoginResultDto> LoginAsync(
        string username,
        string password,
        string? ipAddress = null,
        CancellationToken cancellationToken = default);
}
