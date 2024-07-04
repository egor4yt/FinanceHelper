using FinanceHelper.Persistence;

namespace FinanceHelper.Application.UnitTesting.Common;

public class TestBase : IDisposable
{
    protected readonly ApplicationDbContext ApplicationDbContext = ApplicationContextFactory.Create();

    public async void Dispose()
    {
        await ApplicationContextFactory.DestroyAsync(ApplicationDbContext);
    }
}