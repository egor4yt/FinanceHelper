using FinanceHelper.Application.Services.Interfaces;

namespace FinanceHelper.Application.UnitTesting.Common;

public class UnitTestStringLocalizer<TCommand> : IStringLocalizer<TCommand>
{
    public string this[string keyword] => keyword;
    public string this[string keyword, params object[] arguments] => keyword;
}