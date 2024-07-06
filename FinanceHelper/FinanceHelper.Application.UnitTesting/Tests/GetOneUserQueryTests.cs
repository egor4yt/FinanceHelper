using FinanceHelper.Application.Exceptions;
using FinanceHelper.Application.Queries.Users;
using FinanceHelper.Application.UnitTesting.Common;

namespace FinanceHelper.Application.UnitTesting.Tests;

public class GetOneUserQueryTests : TestBase<GetOneUserQueryHandler>
{
    private readonly GetOneUserQueryHandler _handler;

    public GetOneUserQueryTests()
    {
        _handler = new GetOneUserQueryHandler(ApplicationDbContext, Localizer);
    }

    [Fact]
    public async Task Success()
    {
        // Arrange
        var user = await UserGenerator.SeedOneRandomUserAsync();
        var expectedResponse = new GetOneUserQueryResponse
        {
            Id = user.Id,
            Email = user.Email,
            PreferredLocalizationCode = user.PreferredLocalizationCode
        };
        var request = new GetOneUserQueryRequest
        {
            Id = user.Id
        };

        // Act
        var actualResponse = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.Equivalent(expectedResponse, actualResponse);
    }

    [Fact]
    public async Task UserDoesNotExists()
    {
        // Arrange
        var request = new GetOneUserQueryRequest
        {
            Id = -1
        };

        // Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(request, CancellationToken.None));
    }
}