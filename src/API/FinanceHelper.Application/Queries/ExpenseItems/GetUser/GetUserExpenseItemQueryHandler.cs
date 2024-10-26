using FinanceHelper.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceHelper.Application.Queries.ExpenseItems.GetUser;

public class GetUserExpenseItemQueryHandler(ApplicationDbContext applicationDbContext) : IRequestHandler<GetUserExpenseItemQueryRequest, GetUserExpenseItemQueryResponse>
{
    public async Task<GetUserExpenseItemQueryResponse> Handle(GetUserExpenseItemQueryRequest request, CancellationToken cancellationToken)
    {
        var response = new GetUserExpenseItemQueryResponse();

        var expenseItems = await applicationDbContext.ExpenseItems
            .OrderBy(x => x.Name)
            .Join(applicationDbContext.MetadataLocalizations,
                x => x.ExpenseItemType!.LocalizationKeyword,
                y => y.LocalizationKeyword,
                (item, localization) => new
                {
                    item.Id,
                    item.Color,
                    item.OwnerId,
                    item.ExpenseItemTypeCode,
                    item.Name,
                    item.DeletedAt,
                    localization.SupportedLanguageCode,
                    localization.MetadataTypeCode,
                    localization.LocalizedValue
                })
            .Where(x => x.OwnerId == request.OwnerId
                        && x.ExpenseItemTypeCode != null
                        && x.SupportedLanguageCode == request.LocalizationCode
                        && x.DeletedAt == null
                        && x.MetadataTypeCode == Domain.Metadata.MetadataType.ExpenseItemType.Code)
            .ToListAsync(cancellationToken);

        var expenseItemsTags = await applicationDbContext.TagsMap
            .Join(applicationDbContext.Tags,
                x => x.TagId,
                y => y.Id,
                (map, tag) => new
                {
                    tag.Id,
                    tag.Name,
                    tag.OwnerId,
                    map.EntityId,
                    tag.TagTypeCode
                })
            .OrderBy(x => x.Name)
            .Where(x => x.OwnerId == request.OwnerId
                        && x.TagTypeCode == Domain.Metadata.TagType.ExpenseItem.Code
                        && expenseItems.Select(e => e.Id).Contains(x.EntityId))
            .ToListAsync(cancellationToken);

        response.Items = expenseItems.Select(x => new GetUserExpenseItemQueryResponseItem
        {
            Id = x.Id,
            Name = x.Name,
            Color = x.Color,
            Tags = expenseItemsTags
                .Where(tag => tag.EntityId == x.Id)
                .Select(tag => new GetUserExpenseItemQueryResponseTagDto
                {
                    Id = tag.Id,
                    Name = tag.Name
                }).ToList(),
            ExpenseItemType = x.ExpenseItemTypeCode == null
                ? null
                : new GetUserExpenseItemQueryResponseItemTypeDto
                {
                    Name = x.LocalizedValue,
                    Code = x.ExpenseItemTypeCode!
                }
        }).ToList();

        return response;
    }
}