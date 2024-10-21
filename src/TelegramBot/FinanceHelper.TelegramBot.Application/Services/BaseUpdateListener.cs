using FinanceHelper.TelegramBot.Application.Commands;
using FinanceHelper.TelegramBot.Application.Commands.Base;
using FinanceHelper.TelegramBot.Application.Services.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace FinanceHelper.TelegramBot.Application.Services;

public class BaseUpdateListener(ITelegramBotClient botClient) : IUpdatesListener
{
    /// <summary>
    ///     Allowed to listen commands
    /// </summary>
    private readonly List<ICommand> _commands =
    [
        new StartCommand(botClient),
        new AdminCommand(botClient)
    ];

    public async Task SendUpdateAsync(Update update)
    {
        var message = update.Message;
        if (message?.Text == null) return;

        var command = _commands.FirstOrDefault(x => x.Command == message.Text) ?? new UnknownCommand(botClient);
        await command.ExecuteAsync(update);
    }
}