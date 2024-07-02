namespace FinanceHelper.Application.Services.Interfaces;

internal interface IStringLocalizerFactory
{
    IStringLocalizer Create(Type objectToLocalizationType);
}