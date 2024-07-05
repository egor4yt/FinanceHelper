using FinanceHelper.Application.Exceptions;
using FinanceHelper.Application.Services.Interfaces;
using FinanceHelper.Persistence;
using FinanceHelper.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceHelper.Application.Commands.Authorize.WithCredentials;

public class AuthorizeWithCredentialsCommandHandler(ApplicationDbContext applicationDbContext, IStringLocalizer<AuthorizeWithCredentialsCommandHandler> localizer) : IRequestHandler<AuthorizeWithCredentialsCommandRequest, AuthorizeWithCredentialsCommandResponse>
{
    public async Task<AuthorizeWithCredentialsCommandResponse> Handle(AuthorizeWithCredentialsCommandRequest request, CancellationToken cancellationToken)
    {
        var response = new AuthorizeWithCredentialsCommandResponse();
        var user = await applicationDbContext.Users
            .FirstOrDefaultAsync(x =>
                    x.Email == request.Email
                    && x.PasswordHash == request.PasswordHash,
                cancellationToken);

        if (user == null) throw new ForbiddenException(localizer["WrongCredentials"]);

        response.BearerToken = SecurityHelper.GenerateJwtToken(request.JwtDescriptorDetails, new UserJwtDetails(user));

        return response;
    }
}