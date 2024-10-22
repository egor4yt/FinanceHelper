using FinanceHelper.TelegramBot.Application.Commands.Base;
using FinanceHelper.TelegramBot.Application.Services.Localization.Interfaces;
using FinanceHelper.TelegramBot.MessageBroker.MessageBrokers.Base;
using FinanceHelper.TelegramBot.Shared;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace FinanceHelper.TelegramBot.Application.Commands;

public class AdminCommand(ITelegramBotClient botClient, IMessageBroker messageBroker) : BaseCommand(botClient, messageBroker)
{
    public override string? Command => "/admin";

    public override async Task ExecuteAsync(Update update, IStringLocalizer stringLocalizer)
    {
        var languageCode = update.Message?.From?.LanguageCode ?? Constants.DefaultLanguageCode;
        var message = update.Message;

        var chat = message?.Chat;
        if (chat == null) return;

        if (chat.Username == "egor4yt") await BotClient.SendTextMessageAsync(chat.Id, stringLocalizer["YouAreAdmin"]);
        else await BotClient.SendTextMessageAsync(chat.Id, stringLocalizer["AccessDenied", chat.FirstName ?? "anonymous"]);
    }
}