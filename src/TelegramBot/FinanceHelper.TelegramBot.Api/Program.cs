using FinanceHelper.TelegramBot.Api.Configuration;
using FinanceHelper.TelegramBot.Api.Services;
using FinanceHelper.TelegramBot.Application.Configuration;
using FinanceHelper.TelegramBot.Application.Services.Interfaces;
using FinanceHelper.TelegramBot.Shared;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;
using Telegram.Bot;
using Telegram.Bot.Types;

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
    builder.ConfigureApplication();
    builder.Services.AddControllers().AddNewtonsoftJson();
    builder.Services.AddSerilog();

    var app = builder.Build();
    var telegramBot = app.Services.GetRequiredService<ITelegramBotClient>();

    var telegramWebhookUrl = app.Configuration.GetSection(ConfigurationKeys.TelegramBotWebhookUrl);
    if (string.IsNullOrWhiteSpace(telegramWebhookUrl.Value)) throw new NullReferenceException($"Environment variable '{ConfigurationKeys.TelegramBotWebhookUrl}' was null");
    await telegramBot.DeleteWebhookAsync();
    await telegramBot.SetWebhookAsync(telegramWebhookUrl.Value);

    var botChat = await telegramBot.GetMeAsync();
    Log.Information("Telegram bot '{BotName}' successfully connected", botChat.FirstName);

    app.MapHealthChecks("/health", new HealthCheckOptions
    {
        ResponseWriter = HealthCheckService.WriterHealthCheckResponse,
        AllowCachingResponses = false
    });

    // Process telegram updates
    app.MapPost("/", async context =>
    {
        using var bodyReader = new StreamReader(context.Request.Body);
        var json = await bodyReader.ReadToEndAsync();
        var update = Newtonsoft.Json.JsonConvert.DeserializeObject<Update>(json);
        if (update == null) return;

        var distributor = context.RequestServices.GetRequiredService<IUpdatesDistributor>();
        await distributor.SendUpdateAsync(update);
    });

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}