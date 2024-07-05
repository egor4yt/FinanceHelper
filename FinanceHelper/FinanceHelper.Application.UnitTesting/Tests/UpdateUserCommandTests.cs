using FinanceHelper.Application.Commands.Users.Update;
using FinanceHelper.Application.Exceptions;
using FinanceHelper.Application.UnitTesting.Common;
using Microsoft.EntityFrameworkCore;

namespace FinanceHelper.Application.UnitTesting.Tests;

public class UpdateUserCommandHandlerTests : TestBase<UpdateUserCommandHandler>
{
    private readonly UpdateUserCommandHandler _updateHandler;

    public UpdateUserCommandHandlerTests()
    {
        _updateHandler = new UpdateUserCommandHandler(ApplicationDbContext, Localizer);
    }

    [Fact]
    public async Task Success()
    {
        // Arrange
        var newUser = await UserGenerator.SeedOneRandomUserAsync();
        var request = new UpdateUserCommandRequest
        {
            Id = newUser.Id,
            Email = new Guid().ToString(),
            PreferredLocalizationCode = new Guid().ToString(),
            JwtDescriptorDetails = JwtDescriptorDetails
        };

        // Act
        var response = await _updateHandler.Handle(request, CancellationToken.None);
        var updatedUser = await ApplicationDbContext.Users
            .SingleOrDefaultAsync(x => x.Id == newUser.Id
                                       && x.PasswordHash == newUser.PasswordHash
                                       && x.Email == request.Email
                                       && x.PreferredLocalizationCode == request.PreferredLocalizationCode);
        // Assert
        Assert.Multiple(
            () => Assert.NotNull(updatedUser),
            () => Assert.NotEmpty(response.BearerToken));
    }

    [Fact]
    public async Task UserDesNotExists()
    {
        // Arrange
        var request = new UpdateUserCommandRequest
        {
            Id = -1,
            Email = new Guid().ToString(),
            PreferredLocalizationCode = new Guid().ToString(),
            JwtDescriptorDetails = JwtDescriptorDetails
        };

        // Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _updateHandler.Handle(request, CancellationToken.None));
    }
}