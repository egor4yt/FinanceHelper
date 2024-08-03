using MediatR;

namespace FinanceHelper.Application.Commands.ExpenseItems.Create;

public class CreateExpenseItemCommandRequest : IRequest<CreateExpenseItemCommandResponse>
{
    public string Name { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public string ExpenseItemTypeCode { get; set; } = string.Empty;
    public long OwnerId { get; set; }
}