using MediatR;

namespace FinanceHelper.Application.Queries.ExpenseItems.GetUser;

public class GetUserExpenseItemQueryRequest : IRequest<GetUserExpenseItemQueryResponse>
{
    public long OwnerId { get; set; }
    public string LocalizationCode { get; set; }
}