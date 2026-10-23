using Microsoft.EntityFrameworkCore;
using HIS.DAL;
using HIS.BLL;
using HIS.BLL.Services;
using Xunit;

namespace HIS.Tests;

public class BookingServiceTests
{
    [Fact]
    public void BookingService_CanBeInstantiated_WithDbContext()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestHisDb")
            .Options;

        using var context = new AppDbContext(options);

        // Act
        var service = new BookingService(context);

        // Assert
        Assert.NotNull(service);
    }
}
