using FinanceHelper.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FinanceHelper.Application.UnitTesting.Common;

public class ApplicationContextFactory
{
    public static ApplicationDbContext Create()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var context = new ApplicationDbContext(options);
        context.Database.EnsureCreated();

        return context;
    }

    public static void DestroyAsync(ApplicationDbContext context)
    {
        context.Database.EnsureDeleted();
        context.Dispose();
    }
}