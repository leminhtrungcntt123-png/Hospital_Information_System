using HIS.BLL.DTOs;

namespace HIS.BLL.Interfaces;

public interface IDoctorService
{
    Task<IReadOnlyList<DoctorListItemDto>> GetDoctorsAsync(CancellationToken cancellationToken = default);
}