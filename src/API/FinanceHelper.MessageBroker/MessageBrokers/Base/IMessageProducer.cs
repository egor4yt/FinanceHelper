namespace FinanceHelper.MessageBroker.MessageBrokers.Base;

/// <summary>
///     Async message provider
/// </summary>
public interface IMessageProducer
{
    /// <summary>
    ///     Produce message
    /// </summary>
    /// <param name="data">Message</param>
    /// <typeparam name="T">Type of message</typeparam>
    Task ProduceAsync<T>(T data);
}