using Telegram.Bot.Types;

namespace FinanceHelper.TelegramBot.Application.Services.Telegram.Interfaces;

public interface IUpdatesListener
{
    Task SendUpdateAsync(Update update);
}