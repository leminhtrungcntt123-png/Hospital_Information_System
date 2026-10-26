using HIS.DAL;
using HIS.Domain;
using HIS.BLL.DTOs;
using HIS.BLL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HIS.BLL.Services;

public class DoctorService : IDoctorService
{
    private readonly AppDbContext _context;

    public DoctorService(AppDbContext context)
    {
        _context = context;
    }

    // Lấy danh sách bác sĩ kèm chuyên khoa và khoa phòng
    public async Task<IReadOnlyList<DoctorListItemDto>> GetDoctorsAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Doctors
            .AsNoTracking()
            .OrderBy(d => d.FullName)
            .Select(d => new DoctorListItemDto(
                d.Id,
                d.FullName,
                d.Specialty ?? "",
                d.Department.Name,
                d.LicenseNumber ?? ""))
            .ToListAsync(cancellationToken);
    }
}