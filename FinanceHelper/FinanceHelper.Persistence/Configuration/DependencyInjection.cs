using System;
using FinanceHelper.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FinanceHelper.Persistence.Configuration;

public static class DependencyInjection
{
    public static IHostApplicationBuilder ConfigurePersistence(this IHostApplicationBuilder app)
    {
        app.Services.AddHealthChecks().AddDbContextCheck<ApplicationDbContext>("Database");

        app.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(app.Configuration
                    .GetConnectionString(ConfigurationKeys.DatabaseConnection))
                .LogTo(Console.WriteLine, LogLevel.Information)
        );

        return app;
    }
}