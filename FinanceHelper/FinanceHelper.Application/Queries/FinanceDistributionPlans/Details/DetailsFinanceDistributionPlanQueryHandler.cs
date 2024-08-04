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

        response.FactBudget = plan.FactBudget;
        response.PlannedBudget = plan.PlannedBudget;
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
            responseStepGroup.StepFixedExpenses = 0;
            responseStepGroup.StepFloatedExpenses = 0;
            responseStepGroup.Items = [];

            // We need to calculate fixed values before floating values
            foreach (var stepItem in grouping.OrderByDescending(x => x.ValueTypeCode == Domain.Metadata.FinancesDistributionItemValueType.Fixed.Code))
            {
                var responseStepItem = new StepItem();
                responseStepItem.PlannedValue = stepItem.PlannedValue;

                if (stepItem.ValueTypeCode == Domain.Metadata.FinancesDistributionItemValueType.Fixed.Code)
                {
                    responseStepItem.FactFixedValue = stepItem.PlannedValue * budgetFactor;
                    factBudgetRemaining -= responseStepItem.FactFixedValue;
                    stepFixedExpenses += responseStepItem.FactFixedValue;
                    responseStepGroup.StepFixedExpenses += responseStepItem.FactFixedValue;
                }

                if (stepItem.ValueTypeCode == Domain.Metadata.FinancesDistributionItemValueType.Floating.Code)
                {
                    responseStepItem.FactFixedValue = stepItem.PlannedValue / 100 * (stepBudget - stepFixedExpenses);
                    factBudgetRemaining -= responseStepItem.FactFixedValue;
                    responseStepGroup.StepFloatedExpenses += responseStepItem.FactFixedValue;
                    responseStepItem.PlannedValuePostfix = "%";
                }

                responseStepItem.ExpenseItem = new ExpenseItem
                {
                    Id = stepItem.ExpenseItemId,
                    Name = stepItem.ExpenseItem.Name
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