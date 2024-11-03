using System.Text;
using Confluent.Kafka;

namespace FinanceHelper.MessageBroker.MessageBrokers.Kafka.Serialization;

public class KafkaJsonSerializer<T> : ISerializer<T>
{
    public byte[]? Serialize(T data, SerializationContext context)
    {
        if (data == null) return null;

        var json = System.Text.Json.JsonSerializer.Serialize(data);
        return Encoding.UTF8.GetBytes(json);
    }
}