using FinanceHelper.Application.Services.Interfaces;
using FinanceHelper.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceHelper.Application.Queries.FinanceDistributionPlans.GetUser;

public class GetUserFinanceDistributionPlanQueryHandler(ApplicationDbContext applicationDbContext, IStringLocalizer<GetUserFinanceDistributionPlanQueryHandler> stringLocalizer) : IRequestHandler<GetUserFinanceDistributionPlanQueryRequest, GetUserFinanceDistributionPlanQueryResponse>
{
    public async Task<GetUserFinanceDistributionPlanQueryResponse> Handle(GetUserFinanceDistributionPlanQueryRequest request, CancellationToken cancellationToken)
    {
        var response = new GetUserFinanceDistributionPlanQueryResponse();

        var plans = await applicationDbContext.FinanceDistributionPlans
            .Include(x => x.IncomeSource)
            .OrderByDescending(x => x.CreatedAt)
            .Where(x => x.OwnerId == request.OwnerId)
            .ToListAsync(cancellationToken);

        response.Items = plans.Select(x => new GetUserFinanceDistributionPlanQueryResponseItem
        {
            PlanId = x.Id,
            PlannedBudget = Math.Round(x.PlannedBudget, 2),
            FactBudget = Math.Round(x.FactBudget, 2),
            CreatedAt = x.CreatedAt,
            IncomeSourceName = x.IncomeSource.Name
        }).ToList();

        return response;
    }
}