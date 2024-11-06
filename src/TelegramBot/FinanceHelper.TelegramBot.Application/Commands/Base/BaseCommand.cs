using FinanceHelper.TelegramBot.Application.Services.Localization.Interfaces;
using FinanceHelper.TelegramBot.MessageBroker.MessageBrokers.Base;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace FinanceHelper.TelegramBot.Application.Commands.Base;

public abstract class BaseCommand(ITelegramBotClient botClient, IMessageProducer messageProducer) : ICommand
{
    protected readonly ITelegramBotClient BotClient = botClient;
    protected readonly IMessageProducer MessageProducer = messageProducer;

    public abstract string? Command { get; }
    public abstract Task ExecuteAsync(Update update, IStringLocalizer stringLocalizer);
}