using FinanceHelper.TelegramBot.Application.Commands.Base;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace FinanceHelper.TelegramBot.Application.Commands;

public class StartCommand(ITelegramBotClient botClient) : BaseCommand(botClient)
{
    public override string? Command => "/start";

    public override async Task ExecuteAsync(Update update)
    {
        var message = update.Message;
        if (message == null) return;

        await BotClient.SendTextMessageAsync(message.Chat.Id, "Приветствую вас в Finance Helper!");
    }
}