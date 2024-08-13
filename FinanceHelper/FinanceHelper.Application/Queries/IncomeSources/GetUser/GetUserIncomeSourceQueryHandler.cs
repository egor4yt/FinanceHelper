using FinanceHelper.Application.Services.Interfaces;
using FinanceHelper.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceHelper.Application.Queries.IncomeSources.GetUser;

public class GetUserIncomeSourceQueryHandler(ApplicationDbContext applicationDbContext, IStringLocalizer<GetUserIncomeSourceQueryHandler> stringLocalizer) : IRequestHandler<GetUserIncomeSourceQueryRequest, GetUserIncomeSourceQueryResponse>
{
    public async Task<GetUserIncomeSourceQueryResponse> Handle(GetUserIncomeSourceQueryRequest request, CancellationToken cancellationToken)
    {
        var response = new GetUserIncomeSourceQueryResponse();

        var incomeSources = await applicationDbContext.IncomeSources
            .OrderBy(x => x.Name)
            .Join(applicationDbContext.MetadataLocalizations,
                x => x.IncomeSourceType!.LocalizationKeyword,
                y => y.LocalizationKeyword,
                (item, localization) => new
                {
                    item.Id,
                    item.Color,
                    item.OwnerId,
                    item.IncomeSourceTypeCode,
                    item.Name,
                    localization.SupportedLanguageCode,
                    localization.MetadataTypeCode,
                    localization.LocalizedValue
                })
            .Where(x => x.OwnerId == request.OwnerId
                        && x.SupportedLanguageCode == request.LocalizationCode
                        && x.MetadataTypeCode == Domain.Metadata.MetadataType.IncomeSourceType.Code)
            .ToListAsync(cancellationToken);

        response.Items = incomeSources.Select(x => new GetUserIncomeSourceQueryResponseItem
        {
            Id = x.Id,
            Name = x.Name,
            Color = x.Color,
            IncomeSourceType = new GetUserIncomeSourceQueryResponseItemTypeDto
            {
                Name = x.LocalizedValue,
                Code = x.IncomeSourceTypeCode!
            }
        }).ToList();

        return response;
    }
}