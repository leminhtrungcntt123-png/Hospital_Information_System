using HIS.DAL;
using HIS.Domain;
using HIS.BLL.DTOs;
using Microsoft.EntityFrameworkCore;

namespace HIS.BLL;

public class PatientService : IPatientService
{
	private readonly AppDbContext _context;

	public PatientService(AppDbContext context)
	{
		_context = context;
	}

	// Truy vấn danh sách bệnh nhân (có tìm kiếm theo từ khóa)
	public async Task<IReadOnlyList<PatientListItemDto>> GetPatientsAsync(string? search = null, CancellationToken cancellationToken = default)
	{
		var query = _context.Patients.AsNoTracking().AsQueryable();

		if (!string.IsNullOrWhiteSpace(search))
		{
			var keyword = search.Trim();
			query = query.Where(p =>
				EF.Functions.ILike(p.FullName, $"%{keyword}%") ||
				EF.Functions.ILike(p.PhoneNumber, $"%{keyword}%") ||
				(p.NationalId != null && EF.Functions.ILike(p.NationalId, $"%{keyword}%")));
		}

		return await query
			.OrderByDescending(p => p.Id)
			.Select(p => new PatientListItemDto(
				p.Id,
				p.FullName,
				p.DateOfBirth,
				p.Gender.ToGenderVietnamese(), // Dùng Extension Method trong Domain
				p.PhoneNumber,
				p.NationalId ?? "",
				p.Address ?? "",
				p.InsuranceCardNumber ?? ""))
			.ToListAsync(cancellationToken);
	}
}