using System.Diagnostics.CodeAnalysis;
using FinanceHelper.Application.Exceptions;
using FinanceHelper.Application.Services.Localization.Interfaces;
using FinanceHelper.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceHelper.Application.Commands.ExpenseItems.Update;

[SuppressMessage("Performance", "CA1862")]
public class UpdateExpenseItemCommandHandler(ApplicationDbContext applicationDbContext, IStringLocalizer<UpdateExpenseItemCommandHandler> stringLocalizer) : IRequestHandler<UpdateExpenseItemCommandRequest>
{
    public async Task Handle(UpdateExpenseItemCommandRequest request, CancellationToken cancellationToken)
    {
        var isValidExpenseItemTypeCode = await applicationDbContext.ExpenseItemTypes.AnyAsync(x => x.Code == request.ExpenseItemTypeCode, cancellationToken);
        if (isValidExpenseItemTypeCode == false) throw new BadRequestException(stringLocalizer["ExpenseItemTypeDoesNotExists", request.ExpenseItemTypeCode]);

        var expenseItemIsExists = await applicationDbContext.ExpenseItems
            .AnyAsync(x => x.Name.ToLower() == request.Name.ToLower()
                           && x.OwnerId == request.OwnerId
                           && x.Id != request.ExpenseItemId, cancellationToken);
        if (expenseItemIsExists) throw new BadRequestException(stringLocalizer["ExpenseItemExists", request.Name]);

        var expenseItem = await applicationDbContext.ExpenseItems
            .FirstOrDefaultAsync(x => x.OwnerId == request.OwnerId
                                      && x.Id == request.ExpenseItemId, cancellationToken);
        if (expenseItem == null) throw new NotFoundException(stringLocalizer["ExpenseItemDoesNotExists", request.ExpenseItemId]);

        expenseItem.Color = request.Color;
        expenseItem.ExpenseItemTypeCode = request.ExpenseItemTypeCode;
        expenseItem.Name = request.Name;

        applicationDbContext.ExpenseItems.Update(expenseItem);
        await applicationDbContext.SaveChangesAsync(cancellationToken);
    }
}