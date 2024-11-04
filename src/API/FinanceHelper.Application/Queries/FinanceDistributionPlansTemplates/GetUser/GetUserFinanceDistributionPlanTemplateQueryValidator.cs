using FluentValidation;

namespace FinanceHelper.Application.Queries.FinanceDistributionPlansTemplates.GetUser;

public class GetUserFinanceDistributionPlanTemplateQueryValidator : AbstractValidator<GetUserFinanceDistributionPlanTemplateQueryRequest>
{
    public GetUserFinanceDistributionPlanTemplateQueryValidator()
    {
        RuleFor(x => x.OwnerId)
            .GreaterThan(0);
    }
}