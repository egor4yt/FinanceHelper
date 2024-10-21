using FinanceHelper.TelegramBot.Application.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Telegram.Bot.Types;

namespace FinanceHelper.TelegramBot.Application.Services;

public class UpdatesDistributor(IUpdatesListenerFactory factory, ILogger<UpdatesDistributor> logger) : IUpdatesDistributor
{
    public async Task SendUpdateAsync(Update update)
    {
        if (update.Message == null) throw new NullReferenceException("update.Message was null");
        logger.LogInformation("New message from chat @{ChatName} ({FirstName} {LastName}, {ChatId}): '{Message}'", update.Message.Chat.Username, update.Message.Chat.FirstName, update.Message.Chat.LastName, update.Message.Chat.Id, update.Message.Text);

        var listener = factory.Create(update.Message.Chat.Id);
        await listener.SendUpdateAsync(update);
    }
}