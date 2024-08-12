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
            var stepFloatedExpenses = 0M;
            responseStepGroup.StepNumber = grouping.Key;
            responseStepGroup.Items = [];

            // We need to calculate fixed values before floating values
            foreach (var stepItem in grouping.OrderByDescending(x => x.ValueTypeCode == Domain.Metadata.FinancesDistributionItemValueType.Fixed.Code))
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

                if (stepItem.ValueTypeCode == Domain.Metadata.FinancesDistributionItemValueType.Floating.Code)
                {
                    var value = stepItem.PlannedValue / 100 * (stepBudget - stepFixedExpenses);
                    responseStepItem.FactFixedValue = Math.Round(value, 2);
                    factBudgetRemaining -= value;
                    stepFloatedExpenses += value;
                    responseStepItem.PlannedValuePostfix = "%";
                }

                responseStepItem.ExpenseItem = new ExpenseItem
                {
                    Id = stepItem.ExpenseItemId,
                    Name = stepItem.ExpenseItem.Name
                };

                responseStepGroup.Items.Add(responseStepItem);
            }

            responseStepGroup.StepFixedExpenses = Math.Round(stepFixedExpenses, 2);
            responseStepGroup.StepFloatedExpenses = Math.Round(stepFloatedExpenses, 2);

            response.Steps.Add(responseStepGroup);
        }

        response.Steps = response.Steps.OrderBy(x => x.StepNumber).ToList();
        response.Steps.ForEach(x => x.Items = x.Items.OrderBy(y => y.ExpenseItem.Name).ToList());

        return response;
    }
}