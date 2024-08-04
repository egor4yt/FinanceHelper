using FluentValidation;

namespace FinanceHelper.Application.Queries.FinanceDistributionPlans.Details;

public class DetailsFinanceDistributionPlanQueryValidator : AbstractValidator<DetailsFinanceDistributionPlanQueryRequest>
{
    public DetailsFinanceDistributionPlanQueryValidator()
    {
        RuleFor(x => x.FinanceDistributionPlanId)
            .GreaterThan(0);
        
        RuleFor(x => x.OwnerId)
            .GreaterThan(0);
    }
}