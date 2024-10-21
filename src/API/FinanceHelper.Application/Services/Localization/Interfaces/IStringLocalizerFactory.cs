namespace FinanceHelper.Application.Services.Localization.Interfaces;

internal interface IStringLocalizerFactory
{
    IStringLocalizer Create(Type objectToLocalizationType);
}