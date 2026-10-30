using HIS.BLL.DTOs;
using HIS.BLL.Services;
using HIS.DAL;
using HIS.Domain;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace HIS.Tests;

public class SchedulingServiceTests
{
    private static AppDbContext CreateInMemoryDbContext(string dbName)
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: dbName)
            .Options;

        return new AppDbContext(options);
    }

    [Fact]
    public async Task GenerateSlotsAsync_InvalidDateRange_ReturnsFailure()
    {
        using var context = CreateInMemoryDbContext("Test_InvalidDate");
        var service = new SchedulingService(context);

        var request = new GenerateSlotsRequestDto
        {
            FromDate = new DateOnly(2026, 11, 10),
            ToDate = new DateOnly(2026, 11, 5)
        };

        var result = await service.GenerateSlotsAsync(request);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task GenerateSlotsAsync_GeneratesSlotsAndSkipsExisting()
    {
        using var context = CreateInMemoryDbContext("Test_GenerateSlots");

        var dept = new Department { Id = 1, Name = "Khoa Tim mạch" };
        var room = new ClinicRoom { Id = 1, DepartmentId = 1, RoomName = "Phòng 101", RoomNumber = "P101" };
        var doctor = new Doctor
        {
            Id = 1,
            AccountId = 10,
            DepartmentId = 1,
            FullName = "BS. Nguyễn Văn An",
            Specialty = "Tim mạch",
            LicenseNumber = "BS-001"
        };

        var today = DateOnly.FromDateTime(DateTime.Today);
        var targetDate = today.AddDays(7);
        int targetDow = targetDate.DayOfWeek == DayOfWeek.Sunday ? 7 : (int)targetDate.DayOfWeek;

        var schedule = new DoctorSchedule
        {
            Id = 1,
            DoctorId = 1,
            RoomId = 1,
            DayOfWeek = targetDow,
            Shift = "MORNING",
            StartTime = new TimeOnly(8, 0),
            EndTime = new TimeOnly(9, 0),
            SlotDurationMinutes = 15,
            EffectiveFrom = today,
            EffectiveTo = null,
            IsActive = true
        };

        context.Departments.Add(dept);
        context.ClinicRooms.Add(room);
        context.Doctors.Add(doctor);
        context.DoctorSchedules.Add(schedule);
        await context.SaveChangesAsync();

        var service = new SchedulingService(context);
        var request = new GenerateSlotsRequestDto
        {
            FromDate = targetDate,
            ToDate = targetDate,
            DoctorId = 1
        };

        var result1 = await service.GenerateSlotsAsync(request);
        Assert.True(result1.IsSuccess);
        Assert.Equal(4, result1.TotalGenerated);

        var result2 = await service.GenerateSlotsAsync(request);
        Assert.True(result2.IsSuccess);
        Assert.Equal(0, result2.TotalGenerated);
        Assert.Equal(4, result2.TotalSkippedExisting);

        var slots = await service.GetAvailableSlotsAsync(new SlotSearchFilterDto
        {
            Date = targetDate,
            DoctorId = 1
        });

        Assert.Equal(4, slots.Count);
    }
}
