using FinanceHelper.TelegramBot.Application.Services.Localization;
using FinanceHelper.TelegramBot.Application.Services.Localization.Interfaces;
using FinanceHelper.TelegramBot.Application.Services.Telegram;
using FinanceHelper.TelegramBot.Application.Services.Telegram.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FinanceHelper.TelegramBot.Application.Configuration;

/// <summary>
///     API configuration
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    ///     API configuration
    /// </summary>
    /// <param name="builder">Api instance reference</param>
    public static void ConfigureApplication(this IHostApplicationBuilder builder)
    {
        ConfigureInfrastructure(builder.Services, builder.Configuration);
    }

    private static void ConfigureInfrastructure(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IStringLocalizerFactory, StringLocalizerFactory>();
        services.AddSingleton<IUpdatesListenerFactory, UpdatesListenerFactory>();
        services.AddTransient<IUpdatesDistributor, UpdatesDistributor>();
    }
}