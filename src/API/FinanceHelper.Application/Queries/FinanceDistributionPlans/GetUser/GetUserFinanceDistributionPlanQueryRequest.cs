using MediatR;

namespace FinanceHelper.Application.Queries.FinanceDistributionPlans.GetUser;

public class GetUserFinanceDistributionPlanQueryRequest : IRequest<GetUserFinanceDistributionPlanQueryResponse>
{
    public long OwnerId { get; init; }
}