using MediatR;

namespace FinanceHelper.Application.Commands.ExpenseItems.Create;

public class CreateExpenseItemCommandRequest : IRequest<CreateExpenseItemCommandResponse>
{
    public required string Name { get; init; }
    public required string Color { get; init; }
    public required string ExpenseItemTypeCode { get; init; }
    public long OwnerId { get; init; }
}