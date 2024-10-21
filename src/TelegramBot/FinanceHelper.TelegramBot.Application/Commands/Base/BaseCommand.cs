using FinanceHelper.TelegramBot.Application.Services.Localization.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace FinanceHelper.TelegramBot.Application.Commands.Base;

public abstract class BaseCommand(ITelegramBotClient botClient) : ICommand
{
    protected readonly ITelegramBotClient BotClient = botClient;

    public abstract string? Command { get; }
    public abstract Task ExecuteAsync(Update update, IStringLocalizer stringLocalizer);
}