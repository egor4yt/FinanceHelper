using FinanceHelper.TelegramBot.Api.Filters;
using FinanceHelper.TelegramBot.Api.HealthChecks;
using FinanceHelper.TelegramBot.Shared;
using Telegram.Bot;

namespace FinanceHelper.TelegramBot.Api.Configuration;

/// <summary>
///     API configuration
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    ///     API configuration
    /// </summary>
    /// <param name="builder">Api instance reference</param>
    public static void ConfigureApi(this IHostApplicationBuilder builder)
    {
        ConfigureInfrastructure(builder.Services, builder.Configuration);
        ConfigureTelegram(builder.Services, builder.Configuration);
    }

    private static void ConfigureInfrastructure(IServiceCollection services, IConfiguration configuration)
    {
        services.AddHealthChecks().AddCheck<ApiHealthCheck>("API");
        services.AddControllers(options => options.Filters.Add<ApiExceptionFilterAttribute>());
    }

    private static void ConfigureTelegram(IServiceCollection services, IConfiguration configuration)
    {
        var telegramApiKey = configuration.GetSection(ConfigurationKeys.TelegramBotApiKey);
        if (string.IsNullOrWhiteSpace(telegramApiKey.Value)) throw new NullReferenceException($"Environment variable '{ConfigurationKeys.TelegramBotApiKey}' was null");

        services.AddSingleton<ITelegramBotClient>(new TelegramBotClient(telegramApiKey.Value));
    }
}