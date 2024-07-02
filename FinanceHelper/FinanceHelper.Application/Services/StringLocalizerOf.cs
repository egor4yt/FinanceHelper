using FinanceHelper.Application.Services.Interfaces;

namespace FinanceHelper.Application.Services;

internal class StringLocalizer<T>(IStringLocalizerFactory factory) : IStringLocalizer<T>
{
    private readonly IStringLocalizer _localizer = factory.Create(typeof(T));

    public string this[string keyword] => _localizer[keyword];
    public string this[string keyword, params object[] arguments] => _localizer[keyword, arguments];
}