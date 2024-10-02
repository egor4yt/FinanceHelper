using MediatR;

namespace FinanceHelper.Application.Commands.ExpenseItems.Delete;

public class DeleteExpenseItemCommandRequest : IRequest
{
    public long ExpenseItemId { get; init; }
    public long OwnerId { get; init; }
}