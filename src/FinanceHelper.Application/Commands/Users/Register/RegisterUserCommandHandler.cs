using FinanceHelper.Application.Exceptions;
using FinanceHelper.Application.Services.Interfaces;
using FinanceHelper.Domain.Entities;
using FinanceHelper.Persistence;
using FinanceHelper.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceHelper.Application.Commands.Users.Register;

public class RegisterUserCommandHandler(ApplicationDbContext applicationDbContext, IStringLocalizer<RegisterUserCommandHandler> stringLocalizer) : IRequestHandler<RegisterUserCommandRequest, RegisterUserCommandResponse>
{
    public async Task<RegisterUserCommandResponse> Handle(RegisterUserCommandRequest request, CancellationToken cancellationToken)
    {
        var response = new RegisterUserCommandResponse();
        var userExists = await applicationDbContext.Users.AnyAsync(x => x.Email == request.Email, cancellationToken);
        if (userExists) throw new ConflictException(stringLocalizer["UserAlreadyExists", request.Email]);

        var newUser = new User
        {
            Email = request.Email,
            PreferredLocalizationCode = request.PreferredLocalizationCode,
            PasswordHash = request.PasswordHash,
            FirstName = request.FirstName,
            LastName = request.LastName
        };

        await applicationDbContext.Users.AddAsync(newUser, cancellationToken);
        await applicationDbContext.SaveChangesAsync(cancellationToken);

        response.UserId = newUser.Id;
        response.BearerToken = SecurityHelper.GenerateJwtToken(request.JwtDescriptorDetails, new UserJwtDetails(newUser));

        return response;
    }
}