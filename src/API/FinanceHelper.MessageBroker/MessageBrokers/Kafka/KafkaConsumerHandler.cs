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
    private static readonly Dictionary<string, Type> MessagesToTopicsMap = new Dictionary<string, Type>
    {
        { KafkaTopic.TelegramRegistration, typeof(TelegramRegistrationMessage) }
    };
    private readonly IConsumer<Ignore, object?> _consumer = new ConsumerBuilder<Ignore, object?>(consumerConfig)
        .SetValueDeserializer(new KafkaJsonDeserializer(MessagesToTopicsMap))
        .SetLogHandler((_, msg) => logger.LogDebug("Kafka sent a message with status {KafkaLevel}/{KafkaFacility}. Message: '{Message}'", msg.Level.ToString(), msg.Facility, msg.Message))
        .SetErrorHandler((_, error) => logger.LogError("Kafka couldn't send a message, error code: {KafkaErrorCode}. Reason: '{Reason}'", error.Code, error.Reason))
        .Build();

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _ = ConsumeMessages(cancellationToken);
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _consumer.Unsubscribe();
        _consumer.Close();
        _consumer.Dispose();

        return Task.CompletedTask;
    }

    private async Task ConsumeMessages(CancellationToken cancellationToken)
    {
        var topics = MessagesToTopicsMap.Keys.Distinct().ToList();
        if (topics.Count == 0) return;

        _consumer.Subscribe(topics);

        while (!cancellationToken.IsCancellationRequested)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1000), cancellationToken);

            try
            {
                var consumedResult = _consumer.Consume(cancellationToken);
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

                _consumer.Commit(consumedResult);
            }
            catch (Exception exception)
            {
                logger.LogError(exception, "Kafka consuming error");
            }
        }

        _consumer.Close();
    }
}