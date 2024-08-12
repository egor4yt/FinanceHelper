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
            var sumFloatedValues = planItemsGroup
                .Where(plaItem => plaItem.PlannedValueTypeCode == Domain.Metadata.FinancesDistributionItemValueType.Floating.Code)
                .Sum(plaItem => plaItem.PlannedValue);

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

        foreach (var planItem in request.PlanItems)
        {
            var newPlanItem = new FinanceDistributionPlanItem
            {
                StepNumber = planItem.StepNumber,
                PlannedValue = planItem.PlannedValue,
                ValueTypeCode = planItem.PlannedValueTypeCode
            };
            if (string.IsNullOrWhiteSpace(planItem.NewExpenseItemName)) newPlanItem.ExpenseItemId = planItem.ExpenseItemId!.Value;
            else
                newPlanItem.ExpenseItem = new ExpenseItem
                {
                    Name = planItem.NewExpenseItemName,
                    ExpenseItemTypeCode = null,
                    Color = null,
                    OwnerId = request.OwnerId,
                    Hidden = true
                };

            newFinanceDistributionPlan.FinanceDistributionPlanItems.Add(newPlanItem);
        }

        await applicationDbContext.FinanceDistributionPlans.AddAsync(newFinanceDistributionPlan, cancellationToken);
        await applicationDbContext.SaveChangesAsync(cancellationToken);

        response.Id = newFinanceDistributionPlan.Id;

        return response;
    }
}