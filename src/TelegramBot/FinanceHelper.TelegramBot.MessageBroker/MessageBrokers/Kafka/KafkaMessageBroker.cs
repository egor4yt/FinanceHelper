using Confluent.Kafka;
using FinanceHelper.TelegramBot.MessageBroker.MessageBrokers.Base;
using FinanceHelper.TelegramBot.MessageBroker.Messages.Registration;
using Microsoft.Extensions.Logging;

namespace FinanceHelper.TelegramBot.MessageBroker.MessageBrokers.Kafka;

/// <summary>
///     Kafka message broker
/// </summary>
public class KafkaMessageBroker(ILogger<KafkaMessageBroker> logger, ProducerConfig config) : IMessageBroker
{
    /// <summary>
    ///     Message type : target topics
    /// </summary>
    private readonly Dictionary<Type, string[]> _messagesToTopicsMap = new Dictionary<Type, string[]>
    {
        { typeof(RegisterUser), [KafkaTopic.TelegramRegistration] }
    };

    public async Task SendAsync<T>(T data)
    {
        var topics = GetTopicsForMessage<T>();
        using var producer = new ProducerBuilder<Null, T>(config)
            .SetValueSerializer(new KafkaJsonSerializer<T>())
            .Build();

        foreach (var topic in topics)
        {
            try
            {
                var message = new Message<Null, T> { Value = data };
                var result = await producer.ProduceAsync(topic, message);
                logger.LogDebug("Message of type {Type} sent to topic '{Topic}' with status '{Status}'", typeof(T).ToString(), topic, result.Status);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Kafka can't send message of type '{Type}' to topic '{Topic}'", typeof(T).ToString(), topic);
            }
        }
    }

    /// <summary>
    ///     Get list of topics names for a message
    /// </summary>
    /// <typeparam name="TMessage">Type of message</typeparam>
    /// <returns>Kafka topics list</returns>
    /// <exception cref="InvalidOperationException">Message doesn't supported with kafka provider</exception>
    private string[] GetTopicsForMessage<TMessage>()
    {
        if (_messagesToTopicsMap.TryGetValue(typeof(TMessage), out var topics)) return topics;
        throw new InvalidOperationException("Unsupported message type");
    }
}