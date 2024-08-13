using System.Globalization;
using FinanceHelper.Application.Services.Interfaces;
using FinanceHelper.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceHelper.Application.Queries.ExpenseItems.GetUser;

public class GetUserExpenseItemQueryHandler(ApplicationDbContext applicationDbContext, IStringLocalizer<GetUserExpenseItemQueryHandler> stringLocalizer) : IRequestHandler<GetUserExpenseItemQueryRequest, GetUserExpenseItemQueryResponse>
{
    public async Task<GetUserExpenseItemQueryResponse> Handle(GetUserExpenseItemQueryRequest request, CancellationToken cancellationToken)
    {
        var response = new GetUserExpenseItemQueryResponse();

        var expenseItems = await applicationDbContext.ExpenseItems
            .OrderByDescending(x => x.Name)
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
                    localization.SupportedLanguageCode,
                    localization.MetadataTypeCode,
                    localization.LocalizedValue
                })
            .Where(x => x.OwnerId == request.OwnerId
                        && x.ExpenseItemTypeCode != null
                        && x.SupportedLanguageCode == request.LocalizationCode
                        && x.MetadataTypeCode == Domain.Metadata.MetadataType.ExpenseItemType.Code)
            .ToListAsync(cancellationToken);

        response.Items = expenseItems.Select(x => new GetUserExpenseItemQueryResponseItem
        {
            ExpenseItemId = x.Id,
            Name = x.Name,
            Color = x.Color,
            ExpenseItemType = x.ExpenseItemTypeCode == null
                ? null
                : new GetUserExpenseItemQueryResponseItemTypeDto
                {
                    Name = x.LocalizedValue,
                    TypeCode = x.ExpenseItemTypeCode!
                }
        }).ToList();

        return response;
    }
}