using FinanceHelper.TelegramBot.Application.Services.Localization.Interfaces;
using FinanceHelper.TelegramBot.MessageBroker.MessageBrokers.Base;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace FinanceHelper.TelegramBot.Application.Commands.Base;

public abstract class BaseCommand(ITelegramBotClient botClient, IMessageBroker messageBroker) : ICommand
{
    protected readonly ITelegramBotClient BotClient = botClient;
    protected readonly IMessageBroker MessageBroker = messageBroker;

    public abstract string? Command { get; }
    public abstract Task ExecuteAsync(Update update, IStringLocalizer stringLocalizer);
}