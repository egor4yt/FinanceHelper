namespace FinanceHelper.TelegramBot.Application.Services.Localization.Interfaces;

public interface IStringLocalizerFactory
{
    IStringLocalizer Create(Type objectToLocalizationType, string localizationCode);
}