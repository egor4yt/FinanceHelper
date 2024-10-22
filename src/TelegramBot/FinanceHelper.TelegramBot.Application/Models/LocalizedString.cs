namespace FinanceHelper.TelegramBot.Application.Models;

internal class LocalizedString(string keyword, string value)
{
    public string Keyword { get; set; } = keyword;
    public string Value { get; set; } = value;
}