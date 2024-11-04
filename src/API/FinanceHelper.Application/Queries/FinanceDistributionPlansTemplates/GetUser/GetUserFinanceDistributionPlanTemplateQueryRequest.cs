using MediatR;

namespace FinanceHelper.Application.Queries.FinanceDistributionPlansTemplates.GetUser;

public class GetUserFinanceDistributionPlanTemplateQueryRequest : IRequest<GetUserFinanceDistributionPlanTemplateQueryResponse>
{
    public long OwnerId { get; init; }
}