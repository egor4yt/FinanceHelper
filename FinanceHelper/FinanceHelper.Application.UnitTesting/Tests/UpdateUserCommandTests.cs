using FinanceHelper.Application.Commands.Users.Update;
using FinanceHelper.Application.Exceptions;
using FinanceHelper.Application.UnitTesting.Common;
using FinanceHelper.Application.UnitTesting.Generators;
using Microsoft.EntityFrameworkCore;

namespace FinanceHelper.Application.UnitTesting.Tests;

public class UpdateUserCommandHandlerTests : TestBase<UpdateUserCommandHandler>
{
    private readonly UpdateUserCommandHandler _handler;

    public UpdateUserCommandHandlerTests()
    {
        _handler = new UpdateUserCommandHandler(ApplicationDbContext, Localizer);
    }

    [Fact]
    public async Task Success()
    {
        // Arrange
        var newUser = await ApplicationDbContext.SeedOneUserAsync();
        var request = new UpdateUserCommandRequest
        {
            Id = newUser.Id,
            Email = Guid.NewGuid().ToString(),
            PreferredLocalizationCode = Guid.NewGuid().ToString(),
            JwtDescriptorDetails = JwtDescriptorDetails,
            FirstName = Guid.NewGuid().ToString(),
            LastName = Guid.NewGuid().ToString()
        };

        // Act
        var response = await _handler.Handle(request, CancellationToken.None);
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
            Email = Guid.NewGuid().ToString(),
            PreferredLocalizationCode = Guid.NewGuid().ToString(),
            JwtDescriptorDetails = JwtDescriptorDetails,
            FirstName = Guid.NewGuid().ToString(),
            LastName = Guid.NewGuid().ToString()
        };

        // Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(request, CancellationToken.None));
    }

    [Fact]
    public async Task Duplicate()
    {
        // Arrange
        var user1 = await ApplicationDbContext.SeedOneUserAsync();
        var user2 = await ApplicationDbContext.SeedOneUserAsync();
        var request = new UpdateUserCommandRequest
        {
            Id = user1.Id,
            PreferredLocalizationCode = Guid.NewGuid().ToString(),
            Email = user2.Email,
            JwtDescriptorDetails = JwtDescriptorDetails,
            FirstName = Guid.NewGuid().ToString(),
            LastName = Guid.NewGuid().ToString()
        };

        // Assert
        await Assert.ThrowsAsync<ConflictException>(() => _handler.Handle(request, CancellationToken.None));
    }
}