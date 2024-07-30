using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace FinanceHelper.Persistence.Configuration;

public static class DbInitializer
{
    /// <summary>
    ///     Applying pending migrations and seeding data
    /// </summary>
    /// <param name="application">Application</param>
    /// <returns>Application</returns>
    public static IHost UseInitializeDatabase(this IHost application)
    {
        using var serviceScope = application.Services.CreateScope();
        var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        // // only call this method when there are pending migrations
        if (dbContext.Database.GetPendingMigrations().Any())
        {
            Log.Information("Applying  Migrations");
            dbContext.Database.Migrate();
        }

        return application;
    }
}