using FinanceHelper.Application.Commands.Users.Register;
using FinanceHelper.Application.Exceptions;
using FinanceHelper.Application.Queries.Users;
using FinanceHelper.Application.UnitTesting.Common;
using FinanceHelper.Shared;

namespace FinanceHelper.Application.UnitTesting.Tests;

public class GetOneUserQueryTests : TestBase
{
    private readonly GetOneUserQueryHandler _getOneUserHandler;
    private readonly RegisterUserCommandHandler _registerHandler;

    public GetOneUserQueryTests()
    {
        var registerHandlerLocalizer = StringLocalizerFactory.Create<RegisterUserCommandHandler>();
        var getOneHandlerLocalizer = StringLocalizerFactory.Create<GetOneUserQueryHandler>();

        _registerHandler = new RegisterUserCommandHandler(ApplicationDbContext, registerHandlerLocalizer);
        _getOneUserHandler = new GetOneUserQueryHandler(ApplicationDbContext, getOneHandlerLocalizer);
    }

    [Fact]
    public async Task Success()
    {
        // Arrange
        var registerRequest = new RegisterUserCommandRequest
        {
            PreferredLocalizationCode = "ru",
            Email = "GetOneUserQueryTests_Success@mail.ru",
            PasswordHash = SecurityHelper.ComputeSha256Hash("password"),
            JwtDescriptorDetails = JwtDescriptorDetails
        };

        // Act
        var registerResponse = await _registerHandler.Handle(registerRequest, CancellationToken.None);
        var getOneRequest = new GetOneUserQueryRequest
        {
            Id = registerResponse.UserId
        };
        var expectedResponse = new GetOneUserQueryResponse
        {
            Id = registerResponse.UserId,
            Email = registerRequest.Email,
            PreferredLocalizationCode = registerRequest.PreferredLocalizationCode
        };

        var actualResponse = await _getOneUserHandler.Handle(getOneRequest, CancellationToken.None);

        // Assert
        Assert.Equivalent(expectedResponse, actualResponse);
    }

    [Fact]
    public async Task UserDoesNotExists()
    {
        // Arrange
        var getOneRequest = new GetOneUserQueryRequest
        {
            Id = -1
        };

        // Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _getOneUserHandler.Handle(getOneRequest, CancellationToken.None));
    }
}