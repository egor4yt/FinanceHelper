using FluentValidation;

namespace FinanceHelper.Application.Commands.ExpenseItems.Delete;

public class DeleteExpenseItemCommandValidator : AbstractValidator<DeleteExpenseItemCommandRequest>
{
    public DeleteExpenseItemCommandValidator()
    {
        RuleFor(x => x.OwnerId)
            .GreaterThan(0);

        RuleFor(x => x.ExpenseItemId)
            .GreaterThan(0);
    }
}