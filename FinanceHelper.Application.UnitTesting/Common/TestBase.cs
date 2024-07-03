using FinanceHelper.Application.Services.Interfaces;
using FinanceHelper.Persistence;

namespace FinanceHelper.Application.UnitTesting.Common;

public class TestBase<TCommand> : IDisposable
{
    protected readonly ApplicationDbContext ApplicationDbContext = ApplicationContextFactory.Create();
    protected readonly IStringLocalizer<TCommand> StringLocalizer = StringLocalizerFactory.Create<TCommand>();

    public async void Dispose()
    {
        await ApplicationContextFactory.DestroyAsync(ApplicationDbContext);
    }
}