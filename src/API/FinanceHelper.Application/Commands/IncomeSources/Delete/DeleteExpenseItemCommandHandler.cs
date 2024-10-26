using FinanceHelper.Application.Exceptions;
using FinanceHelper.Application.Services.Localization.Interfaces;
using FinanceHelper.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceHelper.Application.Commands.IncomeSources.Delete;

public class DeleteIncomeSourceCommandHandler(ApplicationDbContext applicationDbContext, IStringLocalizer<DeleteIncomeSourceCommandHandler> stringLocalizer) : IRequestHandler<DeleteIncomeSourceCommandRequest>
{
    public async Task Handle(DeleteIncomeSourceCommandRequest request, CancellationToken cancellationToken)
    {
        var incomeSource = await applicationDbContext.IncomeSources
            .Include(x => x.FinanceDistributionPlans)
            .FirstOrDefaultAsync(x => x.OwnerId == request.OwnerId
                                      && x.Id == request.IncomeSourceId, cancellationToken);
        if (incomeSource == null) throw new NotFoundException(stringLocalizer["IncomeSourceDoesNotExists", request.IncomeSourceId]);

        if (incomeSource.FinanceDistributionPlans.Count != 0)
        {
            incomeSource.DeletedAt = DateTime.UtcNow;
            applicationDbContext.IncomeSources.Update(incomeSource);
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
                .Where(x => x.TagMap.EntityId == incomeSource.Id
                            && x.Tag.TagTypeCode == Domain.Metadata.TagType.IncomeSource.Code)
                .Select(x => x.TagMap)
                .ToListAsync(cancellationToken);

            applicationDbContext.TagsMap.RemoveRange(tagMapIdsToDelete);
            applicationDbContext.Remove(incomeSource);
        }

        await applicationDbContext.SaveChangesAsync(cancellationToken);
    }
}