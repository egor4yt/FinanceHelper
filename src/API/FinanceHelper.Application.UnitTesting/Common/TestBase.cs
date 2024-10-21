using FinanceHelper.Application.Services.Localization.Interfaces;
using FinanceHelper.Persistence;
using FinanceHelper.Shared;

namespace FinanceHelper.Application.UnitTesting.Common;

public class TestBase<TCommandHandler> : IDisposable
{
    protected readonly ApplicationDbContext ApplicationDbContext = ApplicationContextFactory.Create();
    protected readonly JwtDescriptorDetails JwtDescriptorDetails = new JwtDescriptorDetails
    {
        Key = "a95e4695-db56-4301-93d1-64a255bb0945",
        Audience = "test-audience",
        Issuer = "test-issuer",
        TokenLifetimeInHours = 1
    };
    protected readonly IStringLocalizer<TCommandHandler> Localizer = new UnitTestStringLocalizer<TCommandHandler>();

    protected TestBase()
    {
    }

    public void Dispose()
    {
        ApplicationContextFactory.Destroy(ApplicationDbContext);
    }
}