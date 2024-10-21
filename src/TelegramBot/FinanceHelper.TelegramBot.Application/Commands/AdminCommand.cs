using FinanceHelper.TelegramBot.Application.Commands.Base;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace FinanceHelper.TelegramBot.Application.Commands;

public class AdminCommand(ITelegramBotClient botClient) : BaseCommand(botClient)
{
    public override string? Command => "/admin";

    public override async Task ExecuteAsync(Update update)
    {
        var message = update.Message;

        var chat = message?.Chat;
        if (chat == null) return;

        if (chat.Username == "@egor4yt") await BotClient.SendTextMessageAsync(chat.Id, "Вы администратор!");
        else await BotClient.SendTextMessageAsync(chat.Id, $"No, no, no, mr. {chat.FirstName ?? "anonymous"}, you go to the main menu.");
    }
}