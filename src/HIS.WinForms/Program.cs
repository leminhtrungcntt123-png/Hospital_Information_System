using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using HIS.DAL;
using HIS.BLL;
using HIS.BLL.Interfaces;
using HIS.BLL.Services;
using HIS.BLL.Session;

namespace HIS.WinForms;

static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();

        using IHost host = Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration((context, config) =>
            {
                config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            })
            .ConfigureServices((context, services) =>
            {
                var connectionString = context.Configuration.GetConnectionString("DefaultConnection")
                    ?? "Host=localhost;Port=5432;Database=hospital_his;Username=postgres;Password=123456";

                // Register DbContext with PostgreSQL & snake_case naming convention
                services.AddDbContext<AppDbContext>(options =>
                {
                    options.UseNpgsql(connectionString)
                           .UseSnakeCaseNamingConvention();
                });

                // Register BLL services
                services.AddScoped<IAuthService, AuthService>();
                services.AddScoped<IBookingService, BookingService>();
                services.AddScoped<IAdminService, AdminService>();
                services.AddScoped<IReceptionService, ReceptionService>();
                services.AddScoped<IDashboardService, DashboardService>();
                services.AddSingleton<CurrentUser>();

                // Register Forms
                services.AddTransient<LoginForm>();
                services.AddTransient<MainForm>();
            })
            .Build();

        while (true)
        {
            using IServiceScope scope = host.Services.CreateScope();

            var loginForm = scope.ServiceProvider.GetRequiredService<LoginForm>();
            if (loginForm.ShowDialog() != DialogResult.OK || loginForm.AuthenticatedUser == null)
            {
                break;
            }

            var currentUser = scope.ServiceProvider.GetRequiredService<CurrentUser>();
            currentUser.SignIn(loginForm.AuthenticatedUser);

            var mainForm = scope.ServiceProvider.GetRequiredService<MainForm>();
            Application.Run(mainForm);

            if (!mainForm.LogoutRequested)
            {
                break;
            }

            currentUser.SignOut();
        }
    }
}
