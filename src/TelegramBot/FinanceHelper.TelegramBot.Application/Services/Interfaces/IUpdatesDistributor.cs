using Telegram.Bot.Types;

namespace FinanceHelper.TelegramBot.Application.Services.Interfaces;

public interface IUpdatesDistributor
{
    Task SendUpdateAsync(Update update);
}