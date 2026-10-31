using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using HIS.DAL;
using HIS.BLL;
using HIS.BLL.Interfaces;
using HIS.BLL.Services;

namespace HIS.WinForms;

static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();

        var host = Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration((context, config) =>
            {
                config.SetBasePath(AppContext.BaseDirectory);
                config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                config.AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true);
                config.AddEnvironmentVariables();
            })
            .ConfigureServices((context, services) =>
            {
                var connectionString = context.Configuration.GetConnectionString("DefaultConnection")
                    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found in configuration or environment variables.");

                // Register DbContext with PostgreSQL & snake_case naming convention
                services.AddDbContext<AppDbContext>(options =>
                {
                    options.UseNpgsql(connectionString)
                           .UseSnakeCaseNamingConvention();
                });

                // Register BLL services
                services.AddScoped<IBookingService, BookingService>();
                services.AddScoped<IAdminService, AdminService>();
                services.AddScoped<IDashboardService, DashboardService>();
                services.AddScoped<IPatientService, PatientService>();
                services.AddScoped<IDoctorService, DoctorService>();
                services.AddScoped<IAppointmentService, AppointmentService>();
                services.AddScoped<IReceptionService, ReceptionService>();
                services.AddScoped<IStaffService, StaffService>();
                services.AddScoped<IDepartmentService, DepartmentService>();
                services.AddScoped<IAuthService, AuthService>();
                services.AddScoped<ISchedulingService, SchedulingService>();

                // Register Session & Forms
                services.AddScoped<UserSession>();
                services.AddTransient<LoginForm>();
                services.AddTransient<MainForm>();
            })
            .Build();

        // Mỗi phiên đăng nhập dùng một DI scope riêng để DbContext và UserSession được làm mới.
        while (true)
        {
            using var scope = host.Services.CreateScope();

            var loginForm = scope.ServiceProvider.GetRequiredService<LoginForm>();
            if (loginForm.ShowDialog() != DialogResult.OK)
            {
                break;
            }

            var mainForm = scope.ServiceProvider.GetRequiredService<MainForm>();
            Application.Run(mainForm);

            if (!mainForm.LogoutRequested)
            {
                break;
            }
        }
    }
}
