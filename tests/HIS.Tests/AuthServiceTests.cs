using HIS.BLL.DTOs;
using HIS.BLL.Security;
using HIS.BLL.Services;
using HIS.DAL;
using HIS.Domain;
using Microsoft.EntityFrameworkCore;

namespace HIS.Tests;

public class AuthServiceTests
{
    private const string Password = "His@12345";

    [Fact]
    public async Task LoginAsync_WithCorrectCredentials_ReturnsUserWithPermissions()
    {
        using AppDbContext context = CreateContext(nameof(LoginAsync_WithCorrectCredentials_ReturnsUserWithPermissions));
        SeedAccount(context, "td.tranthivan", Password, "RECEPTIONIST");
        var service = new AuthService(context);

        LoginResult result = await service.LoginAsync("td.tranthivan", Password);

        Assert.True(result.Succeeded);
        Assert.NotNull(result.User);
        Assert.Equal("RECEPTIONIST", result.User!.RoleCode);
        Assert.Contains(RolePermissions.Reception, result.User.Permissions);

        Account account = context.Accounts.Single();
        Assert.Equal(0, account.FailedLoginCount);
        Assert.Null(account.LockedUntil);
        Assert.Contains(context.AccessLogs, log => log.Action == "LOGIN_SUCCESS");
    }

    [Fact]
    public async Task LoginAsync_WithWrongPassword_IncrementsFailedCount()
    {
        using AppDbContext context = CreateContext(nameof(LoginAsync_WithWrongPassword_IncrementsFailedCount));
        SeedAccount(context, "td.tranthivan", Password, "RECEPTIONIST");
        var service = new AuthService(context);

        LoginResult result = await service.LoginAsync("td.tranthivan", "sai-mat-khau");

        Assert.False(result.Succeeded);
        Assert.Contains("Còn 4 lần thử", result.ErrorMessage);
        Assert.Equal(1, context.Accounts.Single().FailedLoginCount);
        Assert.Contains(context.AccessLogs, log => log.Action == "LOGIN_FAILED");
    }

    [Fact]
    public async Task LoginAsync_AfterFiveWrongPasswords_LocksAccount()
    {
        using AppDbContext context = CreateContext(nameof(LoginAsync_AfterFiveWrongPasswords_LocksAccount));
        SeedAccount(context, "td.tranthivan", Password, "RECEPTIONIST");
        var service = new AuthService(context);

        for (int attempt = 1; attempt <= AuthService.MaxFailedAttempts; attempt++)
        {
            await service.LoginAsync("td.tranthivan", "sai-mat-khau");
        }

        Account locked = context.Accounts.Single();
        Assert.NotNull(locked.LockedUntil);
        Assert.Equal(0, locked.FailedLoginCount);

        LoginResult result = await service.LoginAsync("td.tranthivan", Password);

        Assert.False(result.Succeeded);
        Assert.Contains("tạm khóa", result.ErrorMessage);
    }

    [Fact]
    public async Task LoginAsync_WithInactiveAccount_IsRejected()
    {
        using AppDbContext context = CreateContext(nameof(LoginAsync_WithInactiveAccount_IsRejected));
        SeedAccount(context, "td.tranthivan", Password, "RECEPTIONIST", isActive: false);
        var service = new AuthService(context);

        LoginResult result = await service.LoginAsync("td.tranthivan", Password);

        Assert.False(result.Succeeded);
        Assert.Contains("vô hiệu hóa", result.ErrorMessage);
    }

    [Fact]
    public async Task LoginAsync_WithPatientRole_IsRejectedFromAdminApp()
    {
        using AppDbContext context = CreateContext(nameof(LoginAsync_WithPatientRole_IsRejectedFromAdminApp));
        SeedAccount(context, "bn0001", Password, "PATIENT");
        var service = new AuthService(context);

        LoginResult result = await service.LoginAsync("bn0001", Password);

        Assert.False(result.Succeeded);
        Assert.Contains("cổng web", result.ErrorMessage);
        Assert.Empty(RolePermissions.For("PATIENT"));
    }

    [Fact]
    public async Task LogoutAsync_RecordsAccessLog()
    {
        using AppDbContext context = CreateContext(nameof(LogoutAsync_RecordsAccessLog));
        SeedAccount(context, "admin", Password, "ADMIN");
        var service = new AuthService(context);

        Account account = context.Accounts.Single();
        await service.LogoutAsync(account.Id);

        Assert.Contains(context.AccessLogs, log => log.AccountId == account.Id && log.Action == "LOGOUT");
    }

    private static AppDbContext CreateContext(string databaseName)
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName)
            .Options;

        return new AppDbContext(options);
    }

    private static void SeedAccount(
        AppDbContext context,
        string username,
        string password,
        string roleCode,
        bool isActive = true)
    {
        Role role = new()
        {
            Id = roleCode switch
            {
                "ADMIN" => 1,
                "DOCTOR" => 2,
                "RECEPTIONIST" => 3,
                _ => 4
            },
            Code = roleCode,
            Name = roleCode
        };

        context.Roles.Add(role);
        context.Accounts.Add(new Account
        {
            Username = username,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password, 4),
            Role = role,
            RoleId = role.Id,
            IsActive = isActive,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        });

        context.SaveChanges();
    }
}
