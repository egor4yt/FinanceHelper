using FluentValidation;

namespace FinanceHelper.Application.Commands.IncomeSources.Update;

public class UpdateIncomeSourceCommandValidator : AbstractValidator<UpdateIncomeSourceCommandRequest>
{
    public UpdateIncomeSourceCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty();

        RuleFor(x => x.IncomeSourceTypeCode)
            .NotEmpty();

        RuleFor(x => x.OwnerId)
            .GreaterThan(0);

        RuleFor(x => x.IncomeSourceId)
            .GreaterThan(0);

        RuleFor(x => x.Color)
            .NotEmpty()
            .Matches(@"^#[\dA-Fa-f]{6}$");
    }
}