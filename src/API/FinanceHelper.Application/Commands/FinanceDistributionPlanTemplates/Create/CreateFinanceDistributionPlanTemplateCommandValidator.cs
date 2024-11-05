using FinanceHelper.Application.Services.Localization.Interfaces;
using FluentValidation;

namespace FinanceHelper.Application.Commands.FinanceDistributionPlanTemplates.Create;

public class CreateFinanceDistributionPlanTemplateCommandValidator : AbstractValidator<CreateFinanceDistributionPlanTemplateCommandRequest>
{
    public CreateFinanceDistributionPlanTemplateCommandValidator(IStringLocalizer<CreateFinanceDistributionPlanTemplateCommandHandler> stringLocalizer)
    {
        RuleFor(x => x.OwnerId)
            .GreaterThan(0);

        RuleFor(x => x.PlannedBudget)
            .GreaterThan(0);

        RuleFor(x => x.Name)
            .NotEmpty();

        When(x => x.FixedPlanItems != null, () =>
        {
            RuleFor(x => x.FixedPlanItems!.Sum(y => y.PlannedValue))
                .LessThan(x => x.PlannedBudget)
                .WithName($"{nameof(CreateFinanceDistributionPlanTemplateCommandRequest.FixedPlanItems)}.Sum(x => x.{nameof(FixedPlanTemplateItem.PlannedValue)})")
                .WithMessage(stringLocalizer["InvalidFixedValue"]);

            RuleForEach(x => x.FixedPlanItems)
                .ChildRules(c =>
                    {
                        c.RuleFor(x => x.ExpenseItemId)
                            .GreaterThan(0);


                        c.RuleFor(x => x.PlannedValue)
                            .GreaterThan(0);
                    }
                );
        });

        RuleFor(x => x.FloatingPlanItems
                .Select(y => y.ExpenseItemId) // select floating expense items
                .Concat(x.FixedPlanItems != null ? x.FixedPlanItems.Select(y => y.ExpenseItemId) : Array.Empty<long>()) // select fixed expense items
                .GroupBy(y => y) // group by expense item id
                .Where(g => g.Count() > 1) // find duplicates
                .Select(g => g.Key) // get duplicates
                .ToList())
            .Must(duplicates => duplicates.Count == 0)
            .WithMessage((_, duplicates) => stringLocalizer["DuplicatedExpenseItems", string.Join(", ", duplicates.Distinct().Order())])
            .WithName($"{nameof(CreateFinanceDistributionPlanTemplateCommandRequest.FixedPlanItems)} and {nameof(CreateFinanceDistributionPlanTemplateCommandRequest.FloatingPlanItems)}");

        RuleForEach(x => x.FloatingPlanItems)
            .NotNull()
            .ChildRules(c =>
                {
                    c.RuleFor(x => x.ExpenseItemId)
                        .GreaterThan(0);


                    c.RuleFor(x => x.PlannedValue)
                        .GreaterThan(0);
                }
            );

        RuleFor(x => x.FloatingPlanItems.Sum(y => y.PlannedValue))
            .Equal(100)
            .WithName($"{nameof(CreateFinanceDistributionPlanTemplateCommandRequest.FloatingPlanItems)}.Sum(x => x.{nameof(FloatingPlanTemplateItem.PlannedValue)})")
            .WithMessage(stringLocalizer["InvalidFloatingValue"]);
    }
}