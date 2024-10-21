using Telegram.Bot.Types;

namespace FinanceHelper.TelegramBot.Application.Services.Interfaces;

public interface IUpdatesListener
{
    Task SendUpdateAsync(Update update);
}