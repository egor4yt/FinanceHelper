using System.Globalization;
using FinanceHelper.Application.Services.Interfaces;

namespace FinanceHelper.Application.Services;

internal class LocalizationManagerStringLocalizer(LocalizationManager localizationManager) : IStringLocalizer
{
    public string this[string keyword]
    {
        get
        {
            var value = localizationManager.GetString(keyword, CultureInfo.CurrentUICulture) ?? keyword;
            return value;
        }
    }

    public string this[string keyword, params object[] arguments]
    {
        get
        {
            var format = localizationManager.GetString(keyword, CultureInfo.CurrentUICulture) ?? keyword;
            var value = string.Format(CultureInfo.CurrentCulture, format, arguments);

            return value;
        }
    }
}