﻿using FinanceHelper.Application.Exceptions;
using FinanceHelper.Application.Services.Localization.Interfaces;
using FinanceHelper.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceHelper.Application.Queries.Users.GetOne;

public class GetOneUserQueryHandler(ApplicationDbContext applicationDbContext, IStringLocalizer<GetOneUserQueryHandler> stringLocalizer) : IRequestHandler<GetOneUserQueryRequest, GetOneUserQueryResponse>
{
    public async Task<GetOneUserQueryResponse> Handle(GetOneUserQueryRequest request, CancellationToken cancellationToken)
    {
        var response = new GetOneUserQueryResponse();

        var user = await applicationDbContext.Users.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (user == null) throw new NotFoundException(stringLocalizer["UserNotFound"], request.Id);

        response.Id = user.Id;
        response.Email = user.Email;
        response.PreferredLocalizationCode = user.PreferredLocalizationCode;
        response.FirstName = user.FirstName;
        response.LastName = user.LastName;

        return response;
    }
}