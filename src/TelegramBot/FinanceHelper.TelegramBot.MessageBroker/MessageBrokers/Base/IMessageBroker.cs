namespace FinanceHelper.TelegramBot.MessageBroker.MessageBrokers.Base;

/// <summary>
///     Message broker provider
/// </summary>
public interface IMessageBroker
{
    /// <summary>
    ///     Produce message
    /// </summary>
    /// <param name="data">Message</param>
    /// <typeparam name="T">Type of message</typeparam>
    Task ProduceAsync<T>(T data);
}