using FinanceHelper.Application.Exceptions;
using FinanceHelper.Application.Services.Interfaces;
using FinanceHelper.Domain.Entities;
using FinanceHelper.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceHelper.Application.Commands.Users.Register;

public class RegisterUserCommandHandler(ApplicationDbContext applicationDbContext, IStringLocalizer<RegisterUserCommandHandler> localizer) : IRequestHandler<RegisterUserCommandRequest, RegisterUserCommandResponse>
{
    public async Task<RegisterUserCommandResponse> Handle(RegisterUserCommandRequest request, CancellationToken cancellationToken)
    {
        var response = new RegisterUserCommandResponse();
        var userExists = await applicationDbContext.Users.AnyAsync(x => x.Email == request.Email, cancellationToken);
        if (userExists) throw new ConflictException(localizer["UserAlreadyExists", request.Email]);

        var newUser = new User
        {
            Email = request.Email,
            PreferredLocalizationCode = request.PreferredLocalizationCode,
            PasswordHash = request.Email
        };

        await applicationDbContext.Users.AddAsync(newUser, cancellationToken);
        await applicationDbContext.SaveChangesAsync(cancellationToken);

        response.Id = newUser.Id;
        return response;
    }
}