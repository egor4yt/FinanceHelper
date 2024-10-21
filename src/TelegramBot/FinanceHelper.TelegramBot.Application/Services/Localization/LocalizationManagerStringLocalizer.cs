using System.Globalization;
using FinanceHelper.TelegramBot.Application.Services.Localization.Interfaces;

namespace FinanceHelper.TelegramBot.Application.Services.Localization;

internal class LocalizationManagerStringLocalizer(LocalizationManager localizationManager, string culture) : IStringLocalizer
{
    public string this[string keyword]
    {
        get
        {
            var value = localizationManager.GetString(keyword, new CultureInfo(culture)) ?? keyword;
            return value;
        }
    }

    public string this[string keyword, params object[] arguments]
    {
        get
        {
            var format = localizationManager.GetString(keyword, new CultureInfo(culture)) ?? keyword;
            var value = string.Format(CultureInfo.CurrentCulture, format, arguments);

            return value;
        }
    }
}