using HIS.BLL.DTOs;

namespace HIS.BLL;

public interface IDoctorService
{
    Task<IReadOnlyList<DoctorListItemDto>> GetDoctorsAsync(CancellationToken cancellationToken = default);
}