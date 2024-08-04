using MediatR;

namespace FinanceHelper.Application.Queries.FinanceDistributionPlans.Details;

public class DetailsFinanceDistributionPlanQueryRequest : IRequest<DetailsFinanceDistributionPlanQueryResponse>
{
    public long FinanceDistributionPlanId { get; set; }
    public long OwnerId { get; set; }
}