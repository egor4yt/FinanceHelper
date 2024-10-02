using FluentValidation;

namespace FinanceHelper.Application.Queries.FinanceDistributionPlans.GetUser;

public class GetUserFinanceDistributionPlanQueryValidator : AbstractValidator<GetUserFinanceDistributionPlanQueryRequest>
{
    public GetUserFinanceDistributionPlanQueryValidator()
    {
        RuleFor(x => x.OwnerId)
            .GreaterThan(0);
    }
}