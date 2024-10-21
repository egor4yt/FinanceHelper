using FinanceHelper.Application.Commands.Users.Register;
using FinanceHelper.Application.Exceptions;
using FinanceHelper.Application.UnitTesting.Common;
using FinanceHelper.Application.UnitTesting.Generators;
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
        var preferredLanguage = await ApplicationDbContext.SeedOneSupportedLanguageAsync();
        var request = new RegisterUserCommandRequest
        {
            PreferredLocalizationCode = preferredLanguage.Code,
            Email = Guid.NewGuid().ToString(),
            PasswordHash = SecurityHelper.ComputeSha256Hash(Guid.NewGuid().ToString()),
            JwtDescriptorDetails = JwtDescriptorDetails,
            FirstName = Guid.NewGuid().ToString(),
            LastName = Guid.NewGuid().ToString()
        };

        // Act
        var response = await _handler.Handle(request, CancellationToken.None);
        var databaseUser = await ApplicationDbContext.Users
            .SingleOrDefaultAsync(x => x.Id == response.UserId
                                       && x.PasswordHash == request.PasswordHash
                                       && x.Email == request.Email
                                       && x.PreferredLocalization.Code == request.PreferredLocalizationCode
                                       && x.FirstName == request.FirstName
                                       && x.LastName == request.LastName);

        // Assert
        Assert.Multiple(
            () => Assert.NotNull(databaseUser),
            () => Assert.NotEmpty(response.BearerToken));
    }

    [Fact]
    public async Task Duplicate()
    {
        // Arrange
        var user = await ApplicationDbContext.SeedOneUserAsync();
        var request = new RegisterUserCommandRequest
        {
            PreferredLocalizationCode = Guid.NewGuid().ToString(),
            Email = user.Email,
            PasswordHash = SecurityHelper.ComputeSha256Hash(Guid.NewGuid().ToString()),
            JwtDescriptorDetails = JwtDescriptorDetails,
            FirstName = Guid.NewGuid().ToString(),
            LastName = Guid.NewGuid().ToString()
        };

        // Assert
        await Assert.ThrowsAsync<ConflictException>(() => _handler.Handle(request, CancellationToken.None));
    }
}