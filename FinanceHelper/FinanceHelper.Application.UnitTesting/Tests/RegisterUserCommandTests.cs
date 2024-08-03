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
            PreferredLocalizationCode = Guid.NewGuid().ToString(),
            Email = Guid.NewGuid().ToString(),
            PasswordHash = SecurityHelper.ComputeSha256Hash(Guid.NewGuid().ToString()),
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
        var user = await UserGenerator.SeedOneAsync();
        var request = new RegisterUserCommandRequest
        {
            PreferredLocalizationCode = Guid.NewGuid().ToString(),
            Email = user.Email,
            PasswordHash = SecurityHelper.ComputeSha256Hash(Guid.NewGuid().ToString()),
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
            PreferredLocalizationCode = Guid.NewGuid().ToString(),
            Email = Guid.NewGuid().ToString(),
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