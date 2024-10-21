using FinanceHelper.Application.Exceptions;
using FinanceHelper.Application.Services.Localization.Interfaces;
using FinanceHelper.Persistence;
using FinanceHelper.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceHelper.Application.Queries.FinanceDistributionPlans.Details;

public class DetailsFinanceDistributionPlanQueryHandler(ApplicationDbContext applicationDbContext, IStringLocalizer<DetailsFinanceDistributionPlanQueryHandler> stringLocalizer) : IRequestHandler<DetailsFinanceDistributionPlanQueryRequest, DetailsFinanceDistributionPlanQueryResponse>
{
    public async Task<DetailsFinanceDistributionPlanQueryResponse> Handle(DetailsFinanceDistributionPlanQueryRequest request, CancellationToken cancellationToken)
    {
        var response = new DetailsFinanceDistributionPlanQueryResponse();
        response.Items = [];
        response.TagsSum = [];

        var tagSum = new Dictionary<string, decimal>();

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
        response.IncomeSource = new DetailsFinanceDistributionPlanQueryResponseIncomeSource
        {
            Id = plan.IncomeSourceId,
            Name = plan.IncomeSource.Name
        };

        var budgetFactor = plan.FactBudget / plan.PlannedBudget;
        var totalFixedExpenses = 0M;

        // We need to calculate fixed values before floating values
        foreach (var planItem in plan.FinanceDistributionPlanItems.OrderBy(x => x.ValueTypeCode == Domain.Metadata.FinancesDistributionItemValueType.Floating.Code))
        {
            var responseItem = new DetailsFinanceDistributionPlanQueryResponseItem();

            decimal factValue;

            if (planItem.ValueTypeCode == Domain.Metadata.FinancesDistributionItemValueType.Fixed.Code)
            {
                responseItem.PlannedValue = planItem.PlannedValue.ToUiString();

                factValue = planItem.PlannedValue * budgetFactor;
                totalFixedExpenses += factValue;
            }
            else if (planItem.ValueTypeCode == Domain.Metadata.FinancesDistributionItemValueType.Floating.Code)
            {
                responseItem.PlannedValue = planItem.PlannedValue.ToUiString() + "%";

                factValue = planItem.PlannedValue / 100 * (plan.FactBudget - totalFixedExpenses);
            }
            else if (planItem.ValueTypeCode == Domain.Metadata.FinancesDistributionItemValueType.FixedIndivisible.Code)
            {
                responseItem.PlannedValue = planItem.PlannedValue.ToUiString();

                factValue = planItem.PlannedValue;
                totalFixedExpenses += factValue;
            }
            else throw new InvalidOperationException("Unknown finances distribution item value type");

            responseItem.FactFixedValue = factValue.ToUiString();

            var responseItemTags = expenseItemsTags.Where(x => x.EntityId == planItem.ExpenseItemId).Select(x => x.Name).ToList();

            foreach (var tag in responseItemTags)
            {
                if (tagSum.TryAdd(tag, factValue) == false)
                    tagSum[tag] += factValue;
            }

            responseItem.ExpenseItem = new ExpenseItem
            {
                Id = planItem.ExpenseItemId,
                Name = planItem.ExpenseItem.Name,
                Tags = responseItemTags
            };

            response.Items.Add(responseItem);
        }

        response.Items = response.Items
            .OrderBy(x => x.ExpenseItem.Name)
            .ToList();
        response.TagsSum = tagSum
            .OrderBy(x => x.Key)
            .ToDictionary(x => x.Key, x => x.Value.ToUiString());

        return response;
    }
}