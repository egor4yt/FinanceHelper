namespace FinanceHelper.TelegramBot.Application.Services.Interfaces;

/// <summary>
///     Factory to access to telegram updates listeners
/// </summary>
public interface IUpdatesListenerFactory
{
    /// <summary>
    ///     Creates or updates listener for user
    /// </summary>
    /// <param name="chatId">Unique chat identifier</param>
    /// <returns>Updates listener</returns>
    IUpdatesListener Create(long chatId);
}