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
            .Include(x => x.FinanceDistributionPlanTemplateItems).ThenInclude(x => x.ExpenseItem)
            .OrderBy(x => x.Name)
            .Where(x => x.OwnerId == request.OwnerId)
            .ToListAsync(cancellationToken);

        response.Items = plans
            .Select(x => new GetUserFinanceDistributionPlanTemplateQueryResponseItem
            {
                TemplateId = x.Id,
                PlannedBudget = Math.Round(x.PlannedBudget, 2),
                Name = x.Name,
                IncomeSource = new GetUserFinanceDistributionPlanTemplateQueryIncomeSource
                {
                    Id = x.IncomeSource.Id,
                    Name = x.IncomeSource.Name
                },
                FloatedExpenseItems = x.FinanceDistributionPlanTemplateItems
                    .Where(item => item.ValueTypeCode == Domain.Metadata.FinancesDistributionItemValueType.Floating.Code)
                    .Select(item => new GetUserFinanceDistributionPlanTemplateQueryFloatedExpenseItem
                    {
                        Id = item.Id,
                        Name = item.ExpenseItem.Name,
                        PlannedValue = item.PlannedValue
                    })
                    .ToList(),
                FixedExpenseItems = x.FinanceDistributionPlanTemplateItems
                    .Where(item => item.ValueTypeCode == Domain.Metadata.FinancesDistributionItemValueType.Fixed.Code
                                   || item.ValueTypeCode == Domain.Metadata.FinancesDistributionItemValueType.FixedIndivisible.Code)
                    .Select(item => new GetUserFinanceDistributionPlanTemplateQueryFixedExpenseItem
                    {
                        Id = item.Id,
                        Name = item.ExpenseItem.Name,
                        PlannedValue = item.PlannedValue,
                        Indivisible = item.ValueTypeCode == Domain.Metadata.FinancesDistributionItemValueType.FixedIndivisible.Code
                    })
                    .ToList()
            })
            .ToList();

        return response;
    }
}