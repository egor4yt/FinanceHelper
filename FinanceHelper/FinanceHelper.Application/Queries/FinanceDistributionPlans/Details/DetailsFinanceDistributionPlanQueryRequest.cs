using MediatR;

namespace FinanceHelper.Application.Queries.FinanceDistributionPlans.Details;

public class DetailsFinanceDistributionPlanQueryRequest : IRequest<DetailsFinanceDistributionPlanQueryResponse>
{
    public long FinanceDistributionPlanId { get; init; }
    public long OwnerId { get; init; }
}