using HIS.BLL.DTOs;

namespace HIS.BLL;

public interface IPatientService
{
    Task<IReadOnlyList<PatientListItemDto>> GetPatientsAsync(string? search = null, CancellationToken cancellationToken = default);
}