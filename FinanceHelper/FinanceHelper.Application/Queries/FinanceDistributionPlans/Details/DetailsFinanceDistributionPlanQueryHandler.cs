using FinanceHelper.Application.Exceptions;
using FinanceHelper.Application.Services.Interfaces;
using FinanceHelper.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceHelper.Application.Queries.FinanceDistributionPlans.Details;

public class DetailsFinanceDistributionPlanQueryHandler(ApplicationDbContext applicationDbContext, IStringLocalizer<DetailsFinanceDistributionPlanQueryHandler> stringLocalizer) : IRequestHandler<DetailsFinanceDistributionPlanQueryRequest, DetailsFinanceDistributionPlanQueryResponse>
{
    public async Task<DetailsFinanceDistributionPlanQueryResponse> Handle(DetailsFinanceDistributionPlanQueryRequest request, CancellationToken cancellationToken)
    {
        var response = new DetailsFinanceDistributionPlanQueryResponse();
        response.Steps = [];

        var plan = await applicationDbContext.FinanceDistributionPlans
            .Include(x => x.IncomeSource)
            .Include(x => x.FinanceDistributionPlanItems).ThenInclude(x => x.ExpenseItem)
            .FirstOrDefaultAsync(x =>
                x.Id == request.FinanceDistributionPlanId
                && x.OwnerId == request.OwnerId, cancellationToken);
        if (plan == null) throw new NotFoundException(stringLocalizer["NotFound", request.FinanceDistributionPlanId]);

        var expenseItemsIds = plan.FinanceDistributionPlanItems.Select(e => e.ExpenseItemId).Distinct();
        var expenseItemsTags = await applicationDbContext.TagsMap
            .Join(applicationDbContext.Tags,
                x => x.TagId,
                y => y.Id,
                (map, tag) => new
                {
                    tag.Id,
                    tag.Name,
                    tag.OwnerId,
                    map.EntityId,
                    tag.TagTypeCode
                })
            .OrderBy(x => x.Name)
            .Where(x => x.OwnerId == request.OwnerId
                        && x.TagTypeCode == Domain.Metadata.TagType.ExpenseItem.Code
                        && expenseItemsIds.Contains(x.EntityId))
            .ToListAsync(cancellationToken);
        
        response.FactBudget = Math.Round(plan.FactBudget, 2);
        response.PlannedBudget = Math.Round(plan.PlannedBudget, 2);
        response.CreatedAt = plan.CreatedAt;
        response.IncomeSource = new IncomeSource
        {
            Id = plan.IncomeSourceId,
            Name = plan.IncomeSource.Name
        };

        var budgetFactor = plan.FactBudget / plan.PlannedBudget;
        var factBudgetRemaining = plan.FactBudget;

        var groupedSteps = plan.FinanceDistributionPlanItems
            .GroupBy(x => x.StepNumber)
            .OrderBy(x => x.Key);

        foreach (var grouping in groupedSteps)
        {
            var responseStepGroup = new StepGroup();
            var stepBudget = factBudgetRemaining;
            var stepFixedExpenses = 0M;
            responseStepGroup.StepNumber = grouping.Key;
            responseStepGroup.Items = [];

            // We need to calculate fixed values before floating values
            foreach (var stepItem in grouping.OrderBy(x => x.ValueTypeCode == Domain.Metadata.FinancesDistributionItemValueType.Floating.Code))
            {
                var responseStepItem = new StepItem();
                responseStepItem.PlannedValue = Math.Round(stepItem.PlannedValue, 2);

                if (stepItem.ValueTypeCode == Domain.Metadata.FinancesDistributionItemValueType.Fixed.Code)
                {
                    var value = stepItem.PlannedValue * budgetFactor;
                    responseStepItem.FactFixedValue = Math.Round(value, 2);
                    factBudgetRemaining -= value;
                    stepFixedExpenses += value;
                }
                else if (stepItem.ValueTypeCode == Domain.Metadata.FinancesDistributionItemValueType.Floating.Code)
                {
                    var value = stepItem.PlannedValue / 100 * (stepBudget - stepFixedExpenses);
                    responseStepItem.FactFixedValue = Math.Round(value, 2);
                    factBudgetRemaining -= value;
                    responseStepItem.PlannedValuePostfix = "%";
                }
                else if (stepItem.ValueTypeCode == Domain.Metadata.FinancesDistributionItemValueType.FixedIndivisible.Code)
                {
                    var value = stepItem.PlannedValue;
                    responseStepItem.FactFixedValue = Math.Round(value, 2);
                    factBudgetRemaining -= value;
                    stepFixedExpenses += value;
                }
                else throw new InvalidOperationException("Unknown finances distribution item value type");

                responseStepItem.ExpenseItem = new ExpenseItem
                {
                    Id = stepItem.ExpenseItemId,
                    Name = stepItem.ExpenseItem.Name,
                    Tags = expenseItemsTags.Where(x => x.EntityId == stepItem.ExpenseItemId).Select(x => x.Name).ToList()
                };

                responseStepGroup.Items.Add(responseStepItem);
            }

            response.Steps.Add(responseStepGroup);
        }

        response.Steps = response.Steps.OrderBy(x => x.StepNumber).ToList();
        response.Steps.ForEach(x => x.Items = x.Items.OrderBy(y => y.ExpenseItem.Name).ToList());

        return response;
    }
}