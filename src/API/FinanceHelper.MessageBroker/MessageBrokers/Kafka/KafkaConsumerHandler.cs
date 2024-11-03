using Confluent.Kafka;
using FinanceHelper.MessageBroker.MessageBrokers.Kafka.Serialization;
using FinanceHelper.MessageBroker.Messages.Base;
using FinanceHelper.MessageBroker.Messages.Telegram.Registration;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FinanceHelper.MessageBroker.MessageBrokers.Kafka;

public class KafkaConsumerHandler(ILogger<KafkaConsumerHandler> logger, ConsumerConfig consumerConfig, IServiceProvider serviceProvider) : IHostedService
{
    private readonly Dictionary<string, Type> _messagesToTopicsMap = new Dictionary<string, Type>
    {
        { KafkaTopic.TelegramRegistration, typeof(TelegramRegistrationMessage) }
    };

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var consumer = new ConsumerBuilder<Ignore, object?>(consumerConfig)
            .SetValueDeserializer(new KafkaJsonDeserializer(_messagesToTopicsMap))
            .Build();

        var topics = _messagesToTopicsMap.Keys.Distinct().ToList();
        if (topics.Count == 0) return;

        consumer.Subscribe(topics);

        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                var consumedResult = consumer.Consume(cancellationToken);
                await using var asyncScope = serviceProvider.CreateAsyncScope();
                var mediator = asyncScope.ServiceProvider.GetRequiredService<IMediator>();

                if (consumedResult.Message.Value is IMessage messageValue)
                {
                    var command = messageValue.ToApplicationCommand();
                    logger.LogDebug("Command: '{@Command}' handled from topic '{Topic}'", command, consumedResult.Topic);
                    await mediator.Send(command, cancellationToken);
                }
                else
                {
                    logger.LogWarning("Message: '{@Message}' from topic '{Topic}' couldn't be converted to application request type", consumedResult.Message.Value, consumedResult.Topic);
                    continue;
                }

                consumer.Commit(consumedResult);
            }
            catch (Exception exception)
            {
                logger.LogError(exception, "Kafka consuming error");
                await Task.Delay(TimeSpan.FromMilliseconds(100), cancellationToken);
            }
        }

        consumer.Close();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}