using MediatR;

namespace FinanceHelper.Application.Queries.IncomeSourceTypes.GetLocalized;

public class GetLocalizedIncomeSourceTypesQueryRequest : IRequest<GetLocalizedIncomeSourceTypesQueryResponse>
{
    public required string LanguageCode { get; init; }
}