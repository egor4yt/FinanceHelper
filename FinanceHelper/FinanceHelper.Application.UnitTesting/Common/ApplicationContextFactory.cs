using FinanceHelper.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FinanceHelper.Application.UnitTesting.Common;

public class ApplicationContextFactory
{
    public static ApplicationDbContext Create()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("InMemoryDatabase")
            .Options;

        var context = new ApplicationDbContext(options);
        context.Database.EnsureCreatedAsync();

        return context;
    }

    public static async Task DestroyAsync(ApplicationDbContext context)
    {
        await context.Database.EnsureDeletedAsync();
        await context.DisposeAsync();
    }
}