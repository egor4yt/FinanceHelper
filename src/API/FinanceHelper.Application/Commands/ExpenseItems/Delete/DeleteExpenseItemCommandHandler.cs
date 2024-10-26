using FinanceHelper.Application.Exceptions;
using FinanceHelper.Application.Services.Localization.Interfaces;
using FinanceHelper.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceHelper.Application.Commands.ExpenseItems.Delete;

public class DeleteExpenseItemCommandHandler(ApplicationDbContext applicationDbContext, IStringLocalizer<DeleteExpenseItemCommandHandler> stringLocalizer) : IRequestHandler<DeleteExpenseItemCommandRequest>
{
    public async Task Handle(DeleteExpenseItemCommandRequest request, CancellationToken cancellationToken)
    {
        var expenseItem = await applicationDbContext.ExpenseItems
            .Include(x => x.FinanceDistributionPlanItems)
            .FirstOrDefaultAsync(x => x.OwnerId == request.OwnerId
                                      && x.Id == request.ExpenseItemId, cancellationToken);
        if (expenseItem == null) throw new NotFoundException(stringLocalizer["ExpenseItemDoesNotExists", request.ExpenseItemId]);

        if (expenseItem.FinanceDistributionPlanItems.Count != 0)
        {
            expenseItem.DeletedAt = DateTime.UtcNow;
            applicationDbContext.ExpenseItems.Update(expenseItem);
        }
        else
        {
            var tagMapIdsToDelete = await applicationDbContext.TagsMap
                .Join(applicationDbContext.Tags,
                    x => x.TagId,
                    y => y.Id,
                    (x, y) => new
                    {
                        TagMap = x,
                        Tag = y
                    })
                .Where(x => x.TagMap.EntityId == expenseItem.Id
                            && x.Tag.TagTypeCode == Domain.Metadata.TagType.ExpenseItem.Code)
                .Select(x => x.TagMap)
                .ToListAsync(cancellationToken);

            applicationDbContext.TagsMap.RemoveRange(tagMapIdsToDelete);
            applicationDbContext.Remove(expenseItem);
        }

        await applicationDbContext.SaveChangesAsync(cancellationToken);
    }
}