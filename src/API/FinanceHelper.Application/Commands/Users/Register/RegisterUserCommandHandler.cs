using FinanceHelper.Application.Exceptions;
using FinanceHelper.Application.Services.Localization.Interfaces;
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

        if (string.IsNullOrWhiteSpace(request.Email) == false)
        {
            var userExists = await applicationDbContext.Users.AnyAsync(x => x.Email == request.Email, cancellationToken);
            if (userExists) throw new ConflictException(stringLocalizer["UserAlreadyExists", request.Email]);
        }

        if (request.TelegramChatId.HasValue)
        {
            var user = await applicationDbContext.Users.FirstOrDefaultAsync(x => x.TelegramChatId == request.TelegramChatId.Value, cancellationToken);
            if (user != null)
            {
                response.UserId = user.Id;
                return response;
            }
        }

        var newUser = new User
        {
            Email = request.Email,
            PasswordHash = request.PasswordHash,
            FirstName = request.FirstName,
            LastName = request.LastName,
            TelegramChatId = request.TelegramChatId
        };

        if (string.IsNullOrWhiteSpace(request.PreferredLocalizationCode) == false)
        {
            var languageExists = await applicationDbContext.SupportedLanguages.AnyAsync(x => x.Code == request.PreferredLocalizationCode, cancellationToken);
            if (languageExists) newUser.PreferredLocalizationCode = request.PreferredLocalizationCode;
        }

        await applicationDbContext.Users.AddAsync(newUser, cancellationToken);
        await applicationDbContext.SaveChangesAsync(cancellationToken);

        response.UserId = newUser.Id;

        if (request.JwtDescriptorDetails != null)
            response.BearerToken = SecurityHelper.GenerateJwtToken(request.JwtDescriptorDetails, new UserJwtDetails(newUser));

        return response;
    }
}