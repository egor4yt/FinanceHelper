using MediatR;

namespace FinanceHelper.Application.Queries.ExpenseItemTypes.GetLocalized;

public class GetLocalizedExpenseItemTypesQueryRequest : IRequest<GetLocalizedExpenseItemTypesQueryResponse>
{
    public required string LanguageCode { get; init; }
}