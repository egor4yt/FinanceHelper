using System.Globalization;
using FinanceHelper.Persistence;
using FinanceHelper.Shared;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;

namespace FinanceHelper.Application.UnitTesting.Common;

public class TestBase : IDisposable
{
    protected readonly ApplicationDbContext ApplicationDbContext = ApplicationContextFactory.Create();
    protected readonly JwtDescriptorDetails JwtDescriptorDetails = new JwtDescriptorDetails
    {
        Key = "a95e4695-db56-4301-93d1-64a255bb0945",
        Audience = "test-audience",
        Issuer = "test-issuer",
        TokenLifetimeInHours = 1
    };
    protected readonly IOptions<RequestLocalizationOptions> RequestLocalizationOptions = new UnitTestOptions<RequestLocalizationOptions>(new RequestLocalizationOptions
    {
        SupportedCultures = new List<CultureInfo>
        {
            new CultureInfo("ru"),
            new CultureInfo("en")
        }
    });

    public async void Dispose()
    {
        ApplicationContextFactory.DestroyAsync(ApplicationDbContext);
    }
}