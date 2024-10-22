using FinanceHelper.TelegramBot.Application.Services.Localization.Interfaces;
using Telegram.Bot.Types;

namespace FinanceHelper.TelegramBot.Application.Commands.Base;

public interface ICommand
{
    /// <summary>
    ///     Command text
    /// </summary>
    string? Command { get; }

    /// <summary>
    ///     Handling update
    /// </summary>
    /// <param name="update">Update details</param>
    /// <param name="stringLocalizer">String localizer</param>
    Task ExecuteAsync(Update update, IStringLocalizer stringLocalizer);
}