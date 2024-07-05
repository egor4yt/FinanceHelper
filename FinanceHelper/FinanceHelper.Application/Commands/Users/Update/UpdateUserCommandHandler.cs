using FinanceHelper.Application.Exceptions;
using FinanceHelper.Application.Services.Interfaces;
using FinanceHelper.Persistence;
using FinanceHelper.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceHelper.Application.Commands.Users.Update;

public class UpdateUserCommandHandler(ApplicationDbContext applicationDbContext, IStringLocalizer<UpdateUserCommandHandler> stringLocalizer) : IRequestHandler<UpdateUserCommandRequest, UpdateUserCommandResponse>
{
    public async Task<UpdateUserCommandResponse> Handle(UpdateUserCommandRequest request, CancellationToken cancellationToken)
    {
        var response = new UpdateUserCommandResponse();

        var user = await applicationDbContext.Users.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (user == null) throw new NotFoundException(stringLocalizer["UserNotFound"], request.Id);

        user.Email = request.Email;
        user.PreferredLocalizationCode = request.PreferredLocalizationCode;

        applicationDbContext.Users.Update(user);
        await applicationDbContext.SaveChangesAsync(cancellationToken);

        response.BearerToken = SecurityHelper.GenerateJwtToken(request.JwtDescriptorDetails, new UserJwtDetails(user));

        return response;
    }
}