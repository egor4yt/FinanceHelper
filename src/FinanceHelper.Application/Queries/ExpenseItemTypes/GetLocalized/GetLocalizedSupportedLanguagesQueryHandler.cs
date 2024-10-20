using FinanceHelper.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceHelper.Application.Queries.ExpenseItemTypes.GetLocalized;

public class GetLocalizedExpenseItemTypesQueryHandler(ApplicationDbContext applicationDbContext) : IRequestHandler<GetLocalizedExpenseItemTypesQueryRequest, GetLocalizedExpenseItemTypesQueryResponse>
{
    public async Task<GetLocalizedExpenseItemTypesQueryResponse> Handle(GetLocalizedExpenseItemTypesQueryRequest request, CancellationToken cancellationToken)
    {
        var response = new GetLocalizedExpenseItemTypesQueryResponse();

        response.Items = await applicationDbContext.ExpenseItemTypes
            .Join(applicationDbContext.MetadataLocalizations,
                x => x.LocalizationKeyword,
                y => y.LocalizationKeyword,
                (item, localization) => new
                {
                    item.Code,
                    localization.SupportedLanguageCode,
                    localization.MetadataTypeCode,
                    localization.LocalizedValue
                })
            .Where(x => x.SupportedLanguageCode == request.LanguageCode)
            .Select(x =>  new GetLocalizedExpenseItemTypesQueryResponseItem
            {
                Code = x.Code,
                Value = x.LocalizedValue
            })
            .ToListAsync(cancellationToken);

        return response;
    }
}