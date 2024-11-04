using FluentValidation;

namespace FinanceHelper.Application.Commands.FinanceDistributionPlanTemplates.Create;

public class CreateFinanceDistributionPlanTemplateCommandValidator : AbstractValidator<CreateFinanceDistributionPlanTemplateCommandRequest>
{
    public CreateFinanceDistributionPlanTemplateCommandValidator()
    {
        RuleFor(x => x.OwnerId)
            .GreaterThan(0);

        RuleFor(x => x.PlannedBudget)
            .GreaterThan(0);

        RuleFor(x => x.Name)
            .NotEmpty();

        RuleForEach(x => x.FixedPlanItems)
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