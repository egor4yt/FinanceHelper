using System.Net;
using Confluent.Kafka;
using FinanceHelper.TelegramBot.MessageBroker.MessageBrokers.Base;
using FinanceHelper.TelegramBot.MessageBroker.MessageBrokers.Kafka;
using FinanceHelper.TelegramBot.Shared;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FinanceHelper.TelegramBot.MessageBroker.Configuration;

/// <summary>
///     API configuration
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    ///     API configuration
    /// </summary>
    /// <param name="builder">Api instance reference</param>
    public static void ConfigureMessageBroker(this IHostApplicationBuilder builder)
    {
        ConfigureInfrastructure(builder.Services, builder.Configuration);
    }

    private static void ConfigureInfrastructure(IServiceCollection services, IConfiguration configuration)
    {
        var messageBroker = configuration.GetSection(ConfigurationKeys.MessageBroker);
        switch (messageBroker.Value)
        {
            case "kafka":
                var connectionString = configuration.GetSection(ConfigurationKeys.KafkaConnectionString);
                if (string.IsNullOrWhiteSpace(connectionString.Value)) throw new NullReferenceException($"Environment variable '{ConfigurationKeys.KafkaConnectionString}' was null");
                var config = new ProducerConfig
                {
                    BootstrapServers = connectionString.Value,
                    ClientId = Dns.GetHostName(),
                    AllowAutoCreateTopics = true,
                    Acks = Acks.All,
                    EnableIdempotence = true,
                    MessageSendMaxRetries = 3,
                    MaxInFlight = 5
                };
                services.AddSingleton(config);
                services.AddSingleton<IMessageBroker, KafkaMessageBroker>();
                break;
            default:
                throw new InvalidOperationException("Unsupported message broker");
        }
    }
}