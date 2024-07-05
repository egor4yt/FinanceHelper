using FinanceHelper.Application.Commands.Authorize.WithCredentials;
using FinanceHelper.Application.Commands.Users.Register;
using FinanceHelper.Application.Exceptions;
using FinanceHelper.Application.UnitTesting.Common;
using FinanceHelper.Shared;

namespace FinanceHelper.Application.UnitTesting.Tests;

public class AuthorizeWithCredentialsCommandTests : TestBase
{
    private readonly AuthorizeWithCredentialsCommandHandler _authorizeHandler;
    private readonly RegisterUserCommandHandler _registerHandler;

    public AuthorizeWithCredentialsCommandTests()
    {
        var authorizeHandlerLocalizer = StringLocalizerFactory.Create<AuthorizeWithCredentialsCommandHandler>();
        var registerHandlerLocalizer = StringLocalizerFactory.Create<RegisterUserCommandHandler>();

        _authorizeHandler = new AuthorizeWithCredentialsCommandHandler(ApplicationDbContext, authorizeHandlerLocalizer);
        _registerHandler = new RegisterUserCommandHandler(ApplicationDbContext, registerHandlerLocalizer);
    }

    [Fact]
    public async Task Success()
    {
        // Arrange
        var userEmail = "AuthorizeWithCredentialsCommandTests_Success@mail.com";
        var userPasswordHash = SecurityHelper.ComputeSha256Hash("password");
        var registerRequest = new RegisterUserCommandRequest
        {
            PreferredLocalizationCode = "ru",
            Email = userEmail,
            PasswordHash = userPasswordHash,
            JwtDescriptorDetails = JwtDescriptorDetails
        };
        var authorizeRequest = new AuthorizeWithCredentialsCommandRequest
        {
            Email = userEmail,
            PasswordHash = userPasswordHash,
            JwtDescriptorDetails = JwtDescriptorDetails
        };

        // Act
        await _registerHandler.Handle(registerRequest, CancellationToken.None);
        var response = await _authorizeHandler.Handle(authorizeRequest, CancellationToken.None);

        // Assert
        Assert.NotNull(response);
    }

    [Fact]
    public async Task WrongCredentials()
    {
        // Arrange
        var userPasswordHash = SecurityHelper.ComputeSha256Hash("password");
        var registerRequest = new RegisterUserCommandRequest
        {
            PreferredLocalizationCode = "ru",
            Email = "AuthorizeWithCredentialsCommandTests_WrongCredentials1@mail.com",
            PasswordHash = userPasswordHash,
            JwtDescriptorDetails = JwtDescriptorDetails
        };
        var authorizeRequest = new AuthorizeWithCredentialsCommandRequest
        {
            Email = "AuthorizeWithCredentialsCommandTests_WrongCredentials2@mail.com",
            PasswordHash = userPasswordHash,
            JwtDescriptorDetails = JwtDescriptorDetails
        };

        // Act
        await _registerHandler.Handle(registerRequest, CancellationToken.None);

        // Assert
        await Assert.ThrowsAsync<ForbiddenException>(() => _authorizeHandler.Handle(authorizeRequest, CancellationToken.None));
    }
}