namespace FinanceHelper.TelegramBot.MessageBroker.MessageBrokers.Base;

/// <summary>
///     Message broker provider
/// </summary>
public interface IMessageBroker
{
    /// <summary>
    ///     Send message to broker
    /// </summary>
    /// <param name="data">Message</param>
    /// <typeparam name="T">Type of message</typeparam>
    Task SendAsync<T>(T data);
}