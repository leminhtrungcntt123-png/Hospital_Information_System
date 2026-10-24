using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace HIS.DAL;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        
        // Placeholder connection string for design-time / migrations
        var connectionString = "Host=localhost;Port=5432;Database=hospital_his;Username=postgres;Password=123456";
        
        optionsBuilder
            .UseNpgsql(connectionString)
            .UseSnakeCaseNamingConvention();

        return new AppDbContext(optionsBuilder.Options);
    }
}
