using FinanceHelper.TelegramBot.Application.Commands;
using FinanceHelper.TelegramBot.Application.Commands.Base;
using FinanceHelper.TelegramBot.Application.Services.Localization.Interfaces;
using FinanceHelper.TelegramBot.Application.Services.Telegram.Interfaces;
using FinanceHelper.TelegramBot.MessageBroker.MessageBrokers.Base;
using FinanceHelper.TelegramBot.Shared;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace FinanceHelper.TelegramBot.Application.Services.Telegram;

internal class BaseUpdateListener(ITelegramBotClient botClient, IStringLocalizerFactory stringLocalizerFactory, IMessageProducer messageProducer) : IUpdatesListener
{
    /// <summary>
    ///     Allowed to listen commands
    /// </summary>
    private readonly List<ICommand> _commands =
    [
        new StartCommand(botClient, messageProducer),
        new AdminCommand(botClient, messageProducer)
    ];

    public async Task SendUpdateAsync(Update update)
    {
        var message = update.Message;
        if (message?.Text == null) return;

        var command = _commands.FirstOrDefault(x => x.Command == message.Text) ?? new UnknownCommand(botClient, messageProducer);
        var stringLocalizer = stringLocalizerFactory.Create(command.GetType(), update.Message?.From?.LanguageCode ?? Constants.DefaultLanguageCode);
        await command.ExecuteAsync(update, stringLocalizer);
    }
}