using FinanceHelper.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace FinanceHelper.Persistence.Configuration;

public static class DependencyInjection
{
    public static IHostApplicationBuilder ConfigurePersistence(this IHostApplicationBuilder app)
    {
        app.Services.AddHealthChecks().AddDbContextCheck<ApplicationDbContext>("Database");

        if (app.Environment.IsDevelopment())
            app.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(app.Configuration
                        .GetConnectionString(ConfigurationKeys.DatabaseConnection))
                    .LogTo(Log.Information, LogLevel.Information, DbContextLoggerOptions.Id | DbContextLoggerOptions.Category)
                    .EnableSensitiveDataLogging()
            );
        else
            app.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(app.Configuration
                        .GetConnectionString(ConfigurationKeys.DatabaseConnection))
                    .LogTo(Log.Information, LogLevel.Information, DbContextLoggerOptions.Id | DbContextLoggerOptions.Category)
            );

        return app;
    }
}