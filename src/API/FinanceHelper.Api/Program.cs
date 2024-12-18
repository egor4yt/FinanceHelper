﻿using System;
using FinanceHelper.Api.Configuration;
using FinanceHelper.Api.Services;
using FinanceHelper.Application.Configuration;
using FinanceHelper.MessageBroker.Configuration;
using FinanceHelper.Persistence.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;

try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Logging.ClearProviders();

    Log.Logger = new LoggerConfiguration()
        .Enrich.FromLogContext()
        .Enrich.WithMachineName()
        .ReadFrom.Configuration(builder.Configuration)
        .CreateLogger();

    Log.Information("Starting web application");

    builder.ConfigureApi();
    builder.ConfigurePersistence();
    builder.ConfigureApplication();
    builder.ConfigureMessageBroker();

    builder.Services.AddSerilog();

    var app = builder.Build();

    app.UseSerilogRequestLogging(options =>
    {
        options.MessageTemplate = "HTTP {RequestMethod} {RequestPath} Status={StatusCode} Elapsed time={Elapsed} ms";
        options.GetLevel = (_, _, _) => LogEventLevel.Debug;
        options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
        {
            diagnosticContext.Set("RequestHost", httpContext.Request.Host.Value);
            diagnosticContext.Set("RequestScheme", httpContext.Request.Scheme);
        };
    });

    var corsOrigin = builder.Configuration.GetSection("CorsOrigins");
    if (string.IsNullOrWhiteSpace(corsOrigin.Value) == false)
        app.UseCors(x =>
            x.AllowAnyHeader()
                .AllowAnyMethod()
                .WithOrigins(corsOrigin.Value)
                .AllowCredentials());

    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseRequestLocalization();

    app.UseInitializeDatabase();

    app.MapHealthChecks("/health", new HealthCheckOptions
    {
        ResponseWriter = HealthCheckService.WriterHealthCheckResponse,
        AllowCachingResponses = false
    });

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();
    app.Run();
}
catch (Exception ex) when (ex is not HostAbortedException && ex.Source != "Microsoft.EntityFrameworkCore.Design") // see https://github.com/dotnet/efcore/issues/29923
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}