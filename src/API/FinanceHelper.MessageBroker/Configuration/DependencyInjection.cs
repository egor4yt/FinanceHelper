using System.Net;
using Confluent.Kafka;
using FinanceHelper.MessageBroker.MessageBrokers.Base;
using FinanceHelper.MessageBroker.MessageBrokers.Kafka;
using FinanceHelper.Shared;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace FinanceHelper.MessageBroker.Configuration;

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
                if (string.IsNullOrWhiteSpace(connectionString.Value))
                {
                    Log.Warning("Message broker disabled: environment variable '{ConnectionString}' was null", ConfigurationKeys.KafkaConnectionString);
                    break;
                }

                var producerConfig = new ProducerConfig
                {
                    BootstrapServers = connectionString.Value,
                    ClientId = Dns.GetHostName(),
                    AllowAutoCreateTopics = true,
                    Acks = Acks.All,
                    EnableIdempotence = true,
                    MaxInFlight = 5
                };
                services.AddSingleton(producerConfig);

                var consumerConfig = new ConsumerConfig
                {
                    BootstrapServers = connectionString.Value,
                    GroupId = "finance-helper-api",
                    ClientId = Dns.GetHostName(),
                    AutoOffsetReset = AutoOffsetReset.Earliest,
                    EnableAutoCommit = false,
                    EnableAutoOffsetStore = false
                };

                services.AddSingleton(consumerConfig);

                services.AddSingleton<IMessageBroker, KafkaMessageBroker>();
                services.AddHostedService<KafkaConsumerHandler>();
                break;
            default:
                Log.Warning("Message broker disabled: unsupported message broker '{MessageBroker}'", messageBroker.Value);
                break;
        }
    }
}