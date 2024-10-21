namespace FinanceHelper.TelegramBot.Application.Services.Localization.Interfaces;

public interface IStringLocalizer
{
    string this[string keyword] { get; }
    string this[string keyword, params object[] arguments] { get; }
}