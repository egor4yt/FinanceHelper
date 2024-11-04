using FinanceHelper.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceHelper.Application.Queries.FinanceDistributionPlansTemplates.GetUser;

public class GetUserFinanceDistributionPlanTemplateQueryHandler(ApplicationDbContext applicationDbContext) : IRequestHandler<GetUserFinanceDistributionPlanTemplateQueryRequest, GetUserFinanceDistributionPlanTemplateQueryResponse>
{
    public async Task<GetUserFinanceDistributionPlanTemplateQueryResponse> Handle(GetUserFinanceDistributionPlanTemplateQueryRequest request, CancellationToken cancellationToken)
    {
        var response = new GetUserFinanceDistributionPlanTemplateQueryResponse();

        var plans = await applicationDbContext.FinanceDistributionPlanTemplates
            .Include(x => x.IncomeSource)
            .OrderBy(x => x.Name)
            .Where(x => x.OwnerId == request.OwnerId)
            .ToListAsync(cancellationToken);

        response.Items = plans.Select(x => new GetUserFinanceDistributionPlanTemplateQueryResponseItem
        {
            TemplateId = x.Id,
            PlannedBudget = Math.Round(x.PlannedBudget, 2),
            Name = x.Name,
            IncomeSource = new GetUserFinanceDistributionPlanTemplateQueryIncomeSource
            {
                Id = x.IncomeSource.Id,
                Name = x.IncomeSource.Name
            },
            FloatedExpenseItems = [], // TODO: add initialization
            FixedExpenseItems = [] // TODO: add initialization
        }).ToList();

        return response;
    }
}