using FinanceHelper.TelegramBot.Application.Commands.Base;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace FinanceHelper.TelegramBot.Application.Commands;

public class UnknownCommand(ITelegramBotClient botClient) : BaseCommand(botClient)
{
    public override string? Command => null;

    public override async Task ExecuteAsync(Update update)
    {
        var message = update.Message;
        if (message == null) return;

        await BotClient.SendTextMessageAsync(message.Chat.Id, "Простите, не понимаю вас.");
    }
}