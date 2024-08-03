using FinanceHelper.Application.Exceptions;
using FinanceHelper.Application.Services.Interfaces;
using FinanceHelper.Domain.Entities;
using FinanceHelper.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceHelper.Application.Commands.IncomeSources.Create;

public class CreateIncomeSourceCommandHandler(ApplicationDbContext applicationDbContext, IStringLocalizer<CreateIncomeSourceCommandHandler> stringLocalizer) : IRequestHandler<CreateIncomeSourceCommandRequest, CreateIncomeSourceCommandResponse>
{
    public async Task<CreateIncomeSourceCommandResponse> Handle(CreateIncomeSourceCommandRequest request, CancellationToken cancellationToken)
    {
        var response = new CreateIncomeSourceCommandResponse();

        var isValidIncomeSourceTypeCode = await applicationDbContext.IncomeSourceTypes.AnyAsync(x => x.Code == request.IncomeSourceTypeCode, cancellationToken);
        if (isValidIncomeSourceTypeCode == false) throw new BadRequestException(stringLocalizer["IncomeSourceTypeDoesNotExists", request.IncomeSourceTypeCode]);

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