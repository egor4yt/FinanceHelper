using FluentValidation;

namespace FinanceHelper.Application.Commands.FinanceDistributionPlans.Create;

public class CreateFinanceDistributionPlanCommandValidator : AbstractValidator<CreateFinanceDistributionPlanCommandRequest>
{
    public CreateFinanceDistributionPlanCommandValidator()
    {
        RuleFor(x => x.OwnerId)
            .GreaterThan(0);

        RuleFor(x => x.FactBudget)
            .GreaterThan(0);

        RuleFor(x => x.PlannedBudget)
            .GreaterThan(0);

        RuleForEach(x => x.FixedPlanItems)
            .NotNull()
            .ChildRules(c =>
                {
                    c.RuleFor(x => x.ExpenseItemId)
                        .GreaterThan(0);


                    c.RuleFor(x => x.PlannedValue)
                        .NotEmpty()
                        .GreaterThan(0);
                }
            );

        RuleForEach(x => x.FloatingPlanItems)
            .NotNull()
            .ChildRules(c =>
                {
                    c.RuleFor(x => x.ExpenseItemId)
                        .GreaterThan(0);


                    c.RuleFor(x => x.PlannedValue)
                        .NotEmpty()
                        .GreaterThan(0);
                }
            );
    }
}