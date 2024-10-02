using FinanceHelper.Application.Exceptions;
using FinanceHelper.Application.Services.Interfaces;
using FinanceHelper.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceHelper.Application.Commands.ExpenseItems.Delete;

public class DeleteExpenseItemCommandHandler(ApplicationDbContext applicationDbContext, IStringLocalizer<DeleteExpenseItemCommandHandler> stringLocalizer) : IRequestHandler<DeleteExpenseItemCommandRequest>
{
    public async Task Handle(DeleteExpenseItemCommandRequest request, CancellationToken cancellationToken)
    {
        var expenseItem = await applicationDbContext.ExpenseItems
            .FirstOrDefaultAsync(x => x.OwnerId == request.OwnerId
                                      && x.Id == request.ExpenseItemId, cancellationToken);
        if (expenseItem == null) throw new NotFoundException(stringLocalizer["ExpenseItemDoesNotExists", request.ExpenseItemId]);

        applicationDbContext.Remove(expenseItem);
        await applicationDbContext.SaveChangesAsync(cancellationToken);
    }
}