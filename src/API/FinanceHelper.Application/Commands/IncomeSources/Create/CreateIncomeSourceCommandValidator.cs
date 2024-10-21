using FluentValidation;

namespace FinanceHelper.Application.Commands.IncomeSources.Create;

public class CreateIncomeSourceCommandValidator : AbstractValidator<CreateIncomeSourceCommandRequest>
{
    public CreateIncomeSourceCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty();

        RuleFor(x => x.IncomeSourceTypeCode)
            .NotEmpty();

        RuleFor(x => x.OwnerId)
            .GreaterThan(0);

        RuleFor(x => x.Color)
            .NotEmpty()
            .Matches(@"^#[\dA-Fa-f]{6}$");
    }
}