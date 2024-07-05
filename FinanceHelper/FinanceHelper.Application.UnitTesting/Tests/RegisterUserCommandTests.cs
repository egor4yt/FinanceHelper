using FinanceHelper.Application.Commands.Users.Register;
using FinanceHelper.Application.Exceptions;
using FinanceHelper.Application.UnitTesting.Common;
using FinanceHelper.Shared;
using Microsoft.EntityFrameworkCore;

namespace FinanceHelper.Application.UnitTesting.Tests;

public class RegisterUserCommandTests : TestBase<RegisterUserCommandHandler>
{
    private readonly RegisterUserCommandHandler _handler;

    public RegisterUserCommandTests()
    {
        _handler = new RegisterUserCommandHandler(ApplicationDbContext, Localizer);
    }

    [Fact]
    public async Task Success()
    {
        // Arrange
        var request = new RegisterUserCommandRequest
        {
            PreferredLocalizationCode = "ru",
            Email = "RegisterUserCommandTests_Success@gmail.com",
            PasswordHash = SecurityHelper.ComputeSha256Hash("password"),
            JwtDescriptorDetails = JwtDescriptorDetails
        };

        // Act
        var response = await _handler.Handle(request, CancellationToken.None);
        var databaseUser = await ApplicationDbContext.Users
            .SingleOrDefaultAsync(x => x.Id == response.UserId
                                       && x.PasswordHash == request.PasswordHash
                                       && x.Email == request.Email
                                       && x.PreferredLocalizationCode == request.PreferredLocalizationCode);

        // Assert
        Assert.Multiple(
            () => Assert.NotNull(databaseUser),
            () => Assert.NotEmpty(response.BearerToken));
    }

    [Fact]
    public async Task Duplicate()
    {
        // Arrange
        var user = await UserGenerator.SeedOneRandomUserAsync();
        var request = new RegisterUserCommandRequest
        {
            PreferredLocalizationCode = "ru",
            Email = user.Email,
            PasswordHash = SecurityHelper.ComputeSha256Hash("password2"),
            JwtDescriptorDetails = JwtDescriptorDetails
        };

        // Assert
        await Assert.ThrowsAsync<ConflictException>(() => _handler.Handle(request, CancellationToken.None));
    }

    [Fact]
    public async Task ValidationTest()
    {
        // Arrange
        var validator = new RegisterUserCommandValidator(RequestLocalizationOptions);
        var request = new RegisterUserCommandRequest
        {
            PreferredLocalizationCode = "test",
            Email = "RegisterUserCommandTests_ValidationTest",
            PasswordHash = "",
            JwtDescriptorDetails = null!
        };
        var expectedInvalidPropertiesNames = new List<string>
        {
            nameof(request.PreferredLocalizationCode),
            nameof(request.Email),
            nameof(request.PasswordHash),
            nameof(request.JwtDescriptorDetails)
        }.Order();

        // Act
        var validationResult = await validator.ValidateAsync(request);
        var actualInvalidPropertiesNames = validationResult.Errors.Select(x => x.PropertyName).Order();

        // Assert
        Assert.Equal(expectedInvalidPropertiesNames, actualInvalidPropertiesNames);
    }
}