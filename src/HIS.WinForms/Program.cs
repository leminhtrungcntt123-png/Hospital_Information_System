using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using HIS.DAL;
using HIS.BLL;

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
                config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            })
            .ConfigureServices((context, services) =>
            {
                var connectionString = context.Configuration.GetConnectionString("DefaultConnection") 
                    ?? "Host=localhost;Port=5432;Database=his_db;Username=postgres;Password=your_password_here";

                // Register DbContext with PostgreSQL & snake_case naming convention
                services.AddDbContext<AppDbContext>(options =>
                {
                    options.UseNpgsql(connectionString)
                           .UseSnakeCaseNamingConvention();
                });

                // Register BLL services
                services.AddScoped<IBookingService, BookingService>();

                // Register Forms
                services.AddTransient<MainForm>();
            })
            .Build();

        using var scope = host.Services.CreateScope();
        var mainForm = scope.ServiceProvider.GetRequiredService<MainForm>();
        Application.Run(mainForm);
    }
}