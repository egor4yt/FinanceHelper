﻿using FinanceHelper.Application.Exceptions;
using FinanceHelper.Application.Services.Interfaces;
using FinanceHelper.Domain.Entities;
using FinanceHelper.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceHelper.Application.Queries.Users;

public class GetOneUserQueryHandler(ApplicationDbContext applicationDbContext, IStringLocalizer<GetOneUserQueryHandler> _stringLocalizer) : IRequestHandler<GetOneUserQueryRequest, GetOneUserQueryResponse>
{
    public async Task<GetOneUserQueryResponse> Handle(GetOneUserQueryRequest request, CancellationToken cancellationToken)
    {
        var response = new GetOneUserQueryResponse();
        var user = await applicationDbContext.Users.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (user == null) throw new NotFoundException(_stringLocalizer["UserNotFound"], request.Id);

        response.Id = user.Id;
        response.Email = user.Email;
        response.PreferredLocalizationCode = user.PreferredLocalizationCode;

        return response;
    }
}