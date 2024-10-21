using Telegram.Bot.Types;

namespace FinanceHelper.TelegramBot.Application.Services.Telegram.Interfaces;

public interface IUpdatesDistributor
{
    Task SendUpdateAsync(Update update);
}