using FinanceHelper.Application.Exceptions;
using FinanceHelper.Domain.Entities;
using FinanceHelper.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceHelper.Application.Queries.Users;

public class GetOneUserQueryHandler(ApplicationDbContext applicationDbContext) : IRequestHandler<GetOneUserQueryRequest, GetOneUserQueryResponse>
{
    public async Task<GetOneUserQueryResponse> Handle(GetOneUserQueryRequest request, CancellationToken cancellationToken)
    {
        var response = new GetOneUserQueryResponse();
        var user = await applicationDbContext.Users.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (user == null) throw new NotFoundException(nameof(User), request.Id);

        response.Id = user.Id;
        response.Email = user.Email;
        response.PreferredLocalizationCode = user.PreferredLocalizationCode;

        return response;
    }
}