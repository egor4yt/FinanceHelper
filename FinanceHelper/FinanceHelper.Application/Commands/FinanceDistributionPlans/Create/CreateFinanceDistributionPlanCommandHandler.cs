using FinanceHelper.Application.Exceptions;
using FinanceHelper.Application.Services.Interfaces;
using FinanceHelper.Domain.Entities;
using FinanceHelper.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceHelper.Application.Commands.FinanceDistributionPlans.Create;

public class CreateFinanceDistributionPlanCommandHandler(ApplicationDbContext applicationDbContext, IStringLocalizer<CreateFinanceDistributionPlanCommandHandler> stringLocalizer) : IRequestHandler<CreateFinanceDistributionPlanCommandRequest, CreateFinanceDistributionPlanCommandResponse>
{
    public async Task<CreateFinanceDistributionPlanCommandResponse> Handle(CreateFinanceDistributionPlanCommandRequest request, CancellationToken cancellationToken)
    {
        var response = new CreateFinanceDistributionPlanCommandResponse();

        var duplicatedExpenseItemsIds = request.PlanItems
            .GroupBy(x => x.ExpenseItemId)
            .Where(x => x.Count() > 1)
            .Select(x => x.Key)
            .ToList();
        if (duplicatedExpenseItemsIds.Count != 0) throw new BadRequestException(stringLocalizer["DuplicatedExpenseItems", string.Join(", ", duplicatedExpenseItemsIds)]);

        var planItemsGroups = request.PlanItems.GroupBy(x => x.StepNumber);
        var maxStepNumber = request.PlanItems.MaxBy(x => x.StepNumber)!.StepNumber;

        foreach (var planItemsGroup in planItemsGroups)
        {
            var sumFloatedValues = 0M;

            foreach (var plaItem in planItemsGroup)
            {
                var typeCode = Domain.Metadata.FinancesDistributionItemValueType.GetTypeFromStringValue(plaItem.PlannedValue).Code;
                var value = Domain.Metadata.FinancesDistributionItemValueType.GetValueFromStringValue(plaItem.PlannedValue);
                if (typeCode == Domain.Metadata.FinancesDistributionItemValueType.Floating.Code) sumFloatedValues += value;
            }

            if ((sumFloatedValues > 90 && planItemsGroup.Key != maxStepNumber)
                || (sumFloatedValues != 100 && planItemsGroup.Key == maxStepNumber)) throw new BadRequestException(stringLocalizer["InvalidFloatingValue"]);
        }

        var existsExpenseItemsIds = await applicationDbContext.ExpenseItems
            .Where(x => request.PlanItems.Select(p => p.ExpenseItemId).Contains(x.Id) && x.OwnerId == request.OwnerId)
            .Select(x => x.Id)
            .ToListAsync(cancellationToken);
        var notExistsExpenseItemsIds = request.PlanItems
            .Where(x => x.ExpenseItemId.HasValue)
            .Select(x => x.ExpenseItemId!.Value)
            .Except(existsExpenseItemsIds)
            .ToList();
        if (notExistsExpenseItemsIds.Count != 0) throw new BadRequestException(stringLocalizer["ExpenseItemsDoesNotExists", string.Join(", ", notExistsExpenseItemsIds)]);

        var newFinanceDistributionPlan = new FinanceDistributionPlan
        {
            PlannedBudget = request.PlannedBudget,
            FactBudget = request.FactBudget,
            CreatedAt = DateTime.UtcNow,
            OwnerId = request.OwnerId,
            IncomeSourceId = request.IncomeSourceId,
            FinanceDistributionPlanItems = new List<FinanceDistributionPlanItem>()
        };

        foreach (var planItemWithNewExpenseItem in request.PlanItems.Where(x => x.ExpenseItemId.HasValue == false))
        {
            var newExpenseItem = new ExpenseItem
            {
                Name = planItemWithNewExpenseItem.NewExpenseItemName!,
                ExpenseItemTypeCode = null,
                Color = null,
                OwnerId = request.OwnerId,
                OneTimeUsable = true
            };
            newFinanceDistributionPlan.FinanceDistributionPlanItems.Add(new FinanceDistributionPlanItem
            {
                StepNumber = planItemWithNewExpenseItem.StepNumber,
                PlannedValue = Domain.Metadata.FinancesDistributionItemValueType.GetValueFromStringValue(planItemWithNewExpenseItem.PlannedValue),
                ValueTypeCode = Domain.Metadata.FinancesDistributionItemValueType.GetTypeFromStringValue(planItemWithNewExpenseItem.PlannedValue).Code,
                ExpenseItem = newExpenseItem
            });
        }

        foreach (var planItem in request.PlanItems.Where(x => x.ExpenseItemId.HasValue))
        {
            newFinanceDistributionPlan.FinanceDistributionPlanItems.Add(new FinanceDistributionPlanItem
            {
                StepNumber = planItem.StepNumber,
                PlannedValue = Domain.Metadata.FinancesDistributionItemValueType.GetValueFromStringValue(planItem.PlannedValue),
                ValueTypeCode = Domain.Metadata.FinancesDistributionItemValueType.GetTypeFromStringValue(planItem.PlannedValue).Code,
                ExpenseItemId = planItem.ExpenseItemId!.Value
            });
        }

        await applicationDbContext.FinanceDistributionPlans.AddAsync(newFinanceDistributionPlan, cancellationToken);
        await applicationDbContext.SaveChangesAsync(cancellationToken);

        response.Id = newFinanceDistributionPlan.Id;

        return response;
    }
}