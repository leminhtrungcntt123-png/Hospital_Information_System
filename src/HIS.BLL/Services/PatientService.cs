using HIS.DAL;
using HIS.Domain;
using HIS.BLL.DTOs;
using HIS.BLL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HIS.BLL.Services;

public class PatientService : IPatientService
{
	private readonly AppDbContext _context;

	public PatientService(AppDbContext context)
	{
		_context = context;
	}

	// Lấy danh sách bệnh nhân theo từ khóa tìm kiếm và giới hạn số lượng bản ghi
	public async Task<IReadOnlyList<PatientListItemDto>> GetPatientsAsync(string? search = null, int limit = 100, CancellationToken cancellationToken = default)
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
			.Take(limit)
			.Select(p => new PatientListItemDto(
				p.Id,
				p.FullName,
				p.DateOfBirth,
				p.Gender.ToGenderVietnamese(),
				p.PhoneNumber,
				p.NationalId ?? "",
				p.Address ?? "",
				p.InsuranceCardNumber ?? ""))
			.ToListAsync(cancellationToken);
	}
}