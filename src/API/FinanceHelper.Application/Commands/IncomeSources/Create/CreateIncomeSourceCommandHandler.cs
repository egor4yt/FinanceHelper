using System.Diagnostics.CodeAnalysis;
using FinanceHelper.Application.Exceptions;
using FinanceHelper.Application.Services.Localization.Interfaces;
using FinanceHelper.Domain.Entities;
using FinanceHelper.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceHelper.Application.Commands.IncomeSources.Create;

[SuppressMessage("Performance", "CA1862")]
public class CreateIncomeSourceCommandHandler(ApplicationDbContext applicationDbContext, IStringLocalizer<CreateIncomeSourceCommandHandler> stringLocalizer) : IRequestHandler<CreateIncomeSourceCommandRequest, CreateIncomeSourceCommandResponse>
{
    public async Task<CreateIncomeSourceCommandResponse> Handle(CreateIncomeSourceCommandRequest request, CancellationToken cancellationToken)
    {
        var response = new CreateIncomeSourceCommandResponse();

        var isValidIncomeSourceTypeCode = await applicationDbContext.IncomeSourceTypes.AnyAsync(x => x.Code == request.IncomeSourceTypeCode, cancellationToken);
        if (isValidIncomeSourceTypeCode == false) throw new BadRequestException(stringLocalizer["IncomeSourceTypeDoesNotExists", request.IncomeSourceTypeCode]);

        var isExists = await applicationDbContext.IncomeSources
            .AnyAsync(x => x.Name.ToLower() == request.Name.ToLower()
                           && x.OwnerId == request.OwnerId, cancellationToken);
        if (isExists) throw new BadRequestException(stringLocalizer["AlreadyExists", request.Name]);

        var newIncomeSource = new IncomeSource
        {
            Name = request.Name,
            IncomeSourceTypeCode = request.IncomeSourceTypeCode,
            Color = request.Color,
            OwnerId = request.OwnerId
        };

        await applicationDbContext.IncomeSources.AddAsync(newIncomeSource, cancellationToken);
        await applicationDbContext.SaveChangesAsync(cancellationToken);

        response.Id = newIncomeSource.Id;

        return response;
    }
}