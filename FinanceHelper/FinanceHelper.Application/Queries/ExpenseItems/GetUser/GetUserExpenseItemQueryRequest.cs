using MediatR;

namespace FinanceHelper.Application.Queries.ExpenseItems.GetUser;

public class GetUserExpenseItemQueryRequest : IRequest<GetUserExpenseItemQueryResponse>
{
    public long OwnerId { get; init; }
    public required string LocalizationCode { get; init; }
}