using FinanceHelper.Application.Exceptions;
using FinanceHelper.Application.Services.Localization.Interfaces;
using FinanceHelper.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceHelper.Application.Commands.IncomeSources.Update;

public class UpdateIncomeSourceCommandHandler(ApplicationDbContext applicationDbContext, IStringLocalizer<UpdateIncomeSourceCommandHandler> stringLocalizer) : IRequestHandler<UpdateIncomeSourceCommandRequest>
{
    public async Task Handle(UpdateIncomeSourceCommandRequest request, CancellationToken cancellationToken)
    {
        var isValidIncomeSourceTypeCode = await applicationDbContext.IncomeSourceTypes.AnyAsync(x => x.Code == request.IncomeSourceTypeCode, cancellationToken);
        if (isValidIncomeSourceTypeCode == false) throw new BadRequestException(stringLocalizer["IncomeSourceTypeDoesNotExists", request.IncomeSourceTypeCode]);

        var incomeSource = await applicationDbContext.IncomeSources
            .FirstOrDefaultAsync(x => x.OwnerId == request.OwnerId
                                      && x.Id == request.IncomeSourceId, cancellationToken);
        if (incomeSource == null) throw new NotFoundException(stringLocalizer["IncomeSourceDoesNotExists", request.IncomeSourceId]);

        incomeSource.Color = request.Color;
        incomeSource.IncomeSourceTypeCode = request.IncomeSourceTypeCode;
        incomeSource.Name = request.Name;

        applicationDbContext.IncomeSources.Update(incomeSource);
        await applicationDbContext.SaveChangesAsync(cancellationToken);
    }
}