using FinanceHelper.Application.Commands.Users.Register;
using FinanceHelper.Application.Exceptions;
using FinanceHelper.Application.UnitTesting.Common;
using FinanceHelper.Shared;
using Microsoft.EntityFrameworkCore;

namespace FinanceHelper.Application.UnitTesting;

public class RegisterUserCommandTests : TestBase<RegisterUserCommandHandler>
{
    private readonly RegisterUserCommandHandler _handler;

    public RegisterUserCommandTests()
    {
        _handler = new RegisterUserCommandHandler(ApplicationDbContext, StringLocalizer);
    }

    [Fact]
    public async Task Success()
    {
        // Arrange
        var request = new RegisterUserCommandRequest
        {
            PreferredLocalizationCode = "ru",
            Email = "test@mail.com",
            PasswordHash = SecurityHelper.ComputeSha256Hash("password")
        };

        // Act
        var response = await _handler.Handle(request, CancellationToken.None);
        var dataBaseUser = await ApplicationDbContext.Users
            .SingleOrDefaultAsync(x => x.Id == response.Id
                                       && x.PasswordHash == request.PasswordHash
                                       && x.Email == request.Email
                                       && x.PreferredLocalizationCode == request.PreferredLocalizationCode);

        // Assert
        Assert.NotNull(dataBaseUser);
    }

    [Fact]
    public async Task Duplicate()
    {
        // Arrange
        var request = new RegisterUserCommandRequest
        {
            PreferredLocalizationCode = "ru",
            Email = "test@mail.com",
            PasswordHash = SecurityHelper.ComputeSha256Hash("password")
        };

        // Act
        await _handler.Handle(request, CancellationToken.None);

        // Assert
        await Assert.ThrowsAsync<ConflictException>(() => _handler.Handle(request, CancellationToken.None));
    }
}