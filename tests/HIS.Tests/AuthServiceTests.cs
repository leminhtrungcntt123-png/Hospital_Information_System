using HIS.BLL;
using HIS.BLL.DTOs;
using HIS.DAL;
using HIS.Domain;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace HIS.Tests;

public class AuthServiceTests
{
    // Mật khẩu chỉ dùng cho tài khoản InMemory trong test, không liên quan
    // tới tài khoản seed thật của database.
    private const string TestPassword = "UnitTest#2026";

    private static AppDbContext CreateContext(string databaseName)
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName)
            .Options;

        return new AppDbContext(options);
    }

    private static Account CreateAccount(
        AppDbContext context,
        string username = "admin",
        string roleCode = "ADMIN",
        bool isActive = true,
        string? passwordHash = null)
    {
        Role? role = context.Roles.FirstOrDefault(r => r.Code == roleCode);
        if (role == null)
        {
            role = new Role { Code = roleCode, Name = roleCode };
            context.Roles.Add(role);
            context.SaveChanges();
        }

        var account = new Account
        {
            Username = username,
            PasswordHash = passwordHash ?? BCrypt.Net.BCrypt.HashPassword(TestPassword, workFactor: 4),
            RoleId = role.Id,
            IsActive = isActive,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };

        context.Accounts.Add(account);
        context.SaveChanges();

        return account;
    }

    [Fact]
    public async Task LoginAsync_WithValidCredentials_ReturnsUserAndWritesAccessLog()
    {
        using var context = CreateContext(nameof(LoginAsync_WithValidCredentials_ReturnsUserAndWritesAccessLog));
        CreateAccount(context, "admin", "ADMIN");
        var service = new AuthService(context);

        LoginResultDto result = await service.LoginAsync("  admin  ", TestPassword);

        Assert.True(result.Success);
        Assert.NotNull(result.User);
        Assert.Equal("admin", result.User!.Username);
        Assert.Equal("ADMIN", result.User.RoleCode);
        Assert.Equal("ADMIN", result.User.RoleName);

        AccessLog log = Assert.Single(context.AccessLogs);
        Assert.Equal("LOGIN", log.Action);
        Assert.Equal(result.User.AccountId, log.AccountId);
    }

    [Fact]
    public async Task LoginAsync_WithWrongPassword_IncreasesFailedCount()
    {
        using var context = CreateContext(nameof(LoginAsync_WithWrongPassword_IncreasesFailedCount));
        Account account = CreateAccount(context);
        var service = new AuthService(context);

        LoginResultDto result = await service.LoginAsync("admin", "sai-mat-khau");

        Assert.False(result.Success);
        Assert.Contains("còn 4 lần thử", result.Message);
        Assert.Equal(1, account.FailedLoginCount);
        Assert.Empty(context.AccessLogs);
    }

    [Fact]
    public async Task LoginAsync_AfterFiveWrongPasswords_LocksAccount()
    {
        using var context = CreateContext(nameof(LoginAsync_AfterFiveWrongPasswords_LocksAccount));
        Account account = CreateAccount(context);
        var service = new AuthService(context);

        for (int attempt = 1; attempt <= AuthService.MaxFailedAttempts; attempt++)
        {
            await service.LoginAsync("admin", "sai-mat-khau");
        }

        Assert.NotNull(account.LockedUntil);
        Assert.True(account.LockedUntil > DateTime.Now);
        Assert.Equal(0, account.FailedLoginCount);

        LoginResultDto whileLocked = await service.LoginAsync("admin", TestPassword);
        Assert.False(whileLocked.Success);
        Assert.Contains("tạm khóa", whileLocked.Message);
    }

    [Fact]
    public async Task LoginAsync_WithInactiveAccount_IsRejected()
    {
        using var context = CreateContext(nameof(LoginAsync_WithInactiveAccount_IsRejected));
        CreateAccount(context, isActive: false);
        var service = new AuthService(context);

        LoginResultDto result = await service.LoginAsync("admin", TestPassword);

        Assert.False(result.Success);
        Assert.Contains("vô hiệu hóa", result.Message);
    }

    [Fact]
    public async Task LoginAsync_WithUnknownUsername_IsRejected()
    {
        using var context = CreateContext(nameof(LoginAsync_WithUnknownUsername_IsRejected));
        var service = new AuthService(context);

        LoginResultDto result = await service.LoginAsync("khong-ton-tai", TestPassword);

        Assert.False(result.Success);
        Assert.Equal("Tên đăng nhập hoặc mật khẩu không đúng.", result.Message);
    }

    [Fact]
    public async Task LoginAsync_WithPlaceholderHash_FailsWithoutThrowing()
    {
        using var context = CreateContext(nameof(LoginAsync_WithPlaceholderHash_FailsWithoutThrowing));
        Account account = CreateAccount(context, passwordHash: "TEST_ACCOUNT_NO_LOGIN");
        var service = new AuthService(context);

        LoginResultDto result = await service.LoginAsync("admin", TestPassword);

        Assert.False(result.Success);
        Assert.Equal(1, account.FailedLoginCount);
    }

    [Fact]
    public async Task LoginAsync_AfterSuccessfulLogin_ResetsFailedCountAndUnlocks()
    {
        using var context = CreateContext(nameof(LoginAsync_AfterSuccessfulLogin_ResetsFailedCountAndUnlocks));
        Account account = CreateAccount(context);
        var service = new AuthService(context);

        await service.LoginAsync("admin", "sai-mat-khau");
        await service.LoginAsync("admin", "sai-mat-khau");
        account.LockedUntil = DateTime.Now.AddMinutes(-1);

        LoginResultDto result = await service.LoginAsync("admin", TestPassword);

        Assert.True(result.Success);
        Assert.Equal(0, account.FailedLoginCount);
        Assert.Null(account.LockedUntil);
    }

    [Fact]
    public async Task LoginAsync_WithBlankCredentials_IsRejected()
    {
        using var context = CreateContext(nameof(LoginAsync_WithBlankCredentials_IsRejected));
        var service = new AuthService(context);

        LoginResultDto result = await service.LoginAsync("   ", "");

        Assert.False(result.Success);
        Assert.Contains("Vui lòng nhập", result.Message);
    }
}
