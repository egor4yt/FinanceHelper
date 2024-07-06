using FinanceHelper.Application.Commands.Authorize.WithCredentials;
using FinanceHelper.Application.Exceptions;
using FinanceHelper.Application.UnitTesting.Common;
using FinanceHelper.Shared;

namespace FinanceHelper.Application.UnitTesting.Tests;

public class AuthorizeWithCredentialsCommandTests : TestBase<AuthorizeWithCredentialsCommandHandler>
{
    private readonly AuthorizeWithCredentialsCommandHandler _handler;

    public AuthorizeWithCredentialsCommandTests()
    {
        _handler = new AuthorizeWithCredentialsCommandHandler(ApplicationDbContext, Localizer);
    }

    [Fact]
    public async Task Success()
    {
        // Arrange
        var user = await UserGenerator.SeedOneRandomUserAsync();
        var request = new AuthorizeWithCredentialsCommandRequest
        {
            Email = user.Email,
            PasswordHash = user.PasswordHash,
            JwtDescriptorDetails = JwtDescriptorDetails
        };

        // Act
        var response = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(response);
    }

    [Fact]
    public async Task WrongCredentials()
    {
        // Arrange
        var user = await UserGenerator.SeedOneRandomUserAsync();
        var request = new AuthorizeWithCredentialsCommandRequest
        {
            Email = user.Email,
            PasswordHash = SecurityHelper.ComputeSha256Hash(Guid.NewGuid().ToString()),
            JwtDescriptorDetails = JwtDescriptorDetails
        };

        // Assert
        await Assert.ThrowsAsync<ForbiddenException>(() => _handler.Handle(request, CancellationToken.None));
    }
}