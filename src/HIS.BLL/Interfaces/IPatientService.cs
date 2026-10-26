using HIS.BLL.DTOs;

namespace HIS.BLL.Interfaces;

public interface IPatientService
{
    Task<IReadOnlyList<PatientListItemDto>> GetPatientsAsync(string? search = null, int limit = 100, CancellationToken cancellationToken = default);
}