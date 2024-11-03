using System.Globalization;
using FinanceHelper.TelegramBot.Application.Commands.Base;
using FinanceHelper.TelegramBot.Application.Services.Localization.Interfaces;
using FinanceHelper.TelegramBot.MessageBroker.MessageBrokers.Base;
using FinanceHelper.TelegramBot.MessageBroker.Messages.Registration;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace FinanceHelper.TelegramBot.Application.Commands;

public class StartCommand(ITelegramBotClient botClient, IMessageBroker messageBroker) : BaseCommand(botClient, messageBroker)
{
    public override string Command => "/start";

    public override async Task ExecuteAsync(Update update, IStringLocalizer stringLocalizer)
    {
        var message = update.Message;
        if (message == null) return;

        await BotClient.SendTextMessageAsync(message.Chat.Id, stringLocalizer["Welcome"]);
        var from = message.From;
        if (from == null) return;

        var registerUser = new RegisterUser();
        registerUser.ChatId = from.Id;
        registerUser.FirstName = from.FirstName;
        registerUser.LastName = from.LastName;
        if (string.IsNullOrWhiteSpace(from.LanguageCode) == false)
            registerUser.PreferredLocalizationCode = new CultureInfo(from.LanguageCode).TwoLetterISOLanguageName;

        await MessageBroker.ProduceAsync(registerUser);
    }
}