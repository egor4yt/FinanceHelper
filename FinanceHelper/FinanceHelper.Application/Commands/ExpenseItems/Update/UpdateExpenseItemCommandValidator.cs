using FluentValidation;

namespace FinanceHelper.Application.Commands.ExpenseItems.Update;

public class UpdateExpenseItemCommandValidator : AbstractValidator<UpdateExpenseItemCommandRequest>
{
    public UpdateExpenseItemCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty();

        RuleFor(x => x.ExpenseItemTypeCode)
            .NotEmpty();

        RuleFor(x => x.OwnerId)
            .GreaterThan(0);

        RuleFor(x => x.ExpenseItemId)
            .GreaterThan(0);

        RuleFor(x => x.Color)
            .NotEmpty()
            .Matches(@"^#[\dA-Fa-f]{6}$");
    }
}