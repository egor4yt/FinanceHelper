using FinanceHelper.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceHelper.Application.Queries.IncomeSourceTypes.GetLocalized;

public class GetLocalizedIncomeSourceTypesQueryHandler(ApplicationDbContext applicationDbContext) : IRequestHandler<GetLocalizedIncomeSourceTypesQueryRequest, GetLocalizedIncomeSourceTypesQueryResponse>
{
    public async Task<GetLocalizedIncomeSourceTypesQueryResponse> Handle(GetLocalizedIncomeSourceTypesQueryRequest request, CancellationToken cancellationToken)
    {
        var response = new GetLocalizedIncomeSourceTypesQueryResponse();

        response.Items = await applicationDbContext.IncomeSourceTypes
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
            .Where(x => x.SupportedLanguageCode == request.LanguageCode && x.MetadataTypeCode == Domain.Metadata.MetadataType.IncomeSourceType.Code)
            .Select(x => new GetLocalizedIncomeSourceTypesQueryResponseItem
            {
                Code = x.Code,
                Value = x.LocalizedValue
            })
            .ToListAsync(cancellationToken);

        return response;
    }
}