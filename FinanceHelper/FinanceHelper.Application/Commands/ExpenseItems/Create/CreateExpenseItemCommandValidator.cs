using FluentValidation;

namespace FinanceHelper.Application.Commands.ExpenseItems.Create;

public class CreateExpenseItemCommandValidator : AbstractValidator<CreateExpenseItemCommandRequest>
{
    public CreateExpenseItemCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty();

        RuleFor(x => x.ExpenseItemTypeCode)
            .NotEmpty();

        RuleFor(x => x.OwnerId)
            .GreaterThan(0);

        RuleFor(x => x.Color)
            .NotEmpty()
            .Matches(@"^#[\dA-Fa-f]{6}$");
    }
}