﻿using System;
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

        try
        {
            var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var pendingMigrations = dbContext.Database.GetPendingMigrations().ToList();
            
            if (pendingMigrations.Count != 0)
            {
                Log.Information("Applying migrations");

                // only call this method when there are pending migrations
                dbContext.Database.Migrate();

                Log.Information("Applyed {Count} migrations", pendingMigrations.Count);
            }
        }
        catch (Exception e)
        {
            Log.Error(e, "Error while applying migrations");
        }

        return application;
    }
}