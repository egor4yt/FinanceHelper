using FluentValidation;

namespace FinanceHelper.Application.Commands.IncomeSources.Delete;

public class DeleteIncomeSourceCommandValidator : AbstractValidator<DeleteIncomeSourceCommandRequest>
{
    public DeleteIncomeSourceCommandValidator()
    {
        RuleFor(x => x.OwnerId)
            .GreaterThan(0);

        RuleFor(x => x.IncomeSourceId)
            .GreaterThan(0);
    }
}