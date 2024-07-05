using Microsoft.Extensions.Options;

namespace FinanceHelper.Application.UnitTesting.Common;

public class UnitTestOptions<TOptions>(TOptions value) : IOptions<TOptions> where TOptions : class
{
    public TOptions Value { get; } = value;
}