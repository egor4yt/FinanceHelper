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
                || (sumFloatedValues != 100 && planItemsGroup.Key == maxStepNumber)) throw new BadRequestException(stringLocalizer["MaxFloatingValueReached"]);
        }

        var existsExpenseItemsIds = await applicationDbContext.ExpenseItems
            .Where(x => request.PlanItems.Select(p => p.ExpenseItemId).Contains(x.Id) && x.OwnerId == request.OwnerId)
            .Select(x => x.Id)
            .ToListAsync(cancellationToken);
        var notExistsExpenseItemsIds = request.PlanItems
            .Select(x => x.ExpenseItemId)
            .Except(existsExpenseItemsIds)
            .ToList();
        if (notExistsExpenseItemsIds.Count != 0) throw new BadRequestException(stringLocalizer["ExpenseItemsDoesNotExists", string.Join(", ", notExistsExpenseItemsIds)]);

        var newFinanceDistributionPlan = new FinanceDistributionPlan
        {
            PlannedBudget = request.PlannedBudget,
            FactBudget = request.FactBudget,
            CreatedAt = DateTime.UtcNow,
            OwnerId = request.OwnerId,
            IncomeSourceId = request.IncomeSourceId
        };

        newFinanceDistributionPlan.FinanceDistributionPlanItems = request.PlanItems
            .Select(x => new FinanceDistributionPlanItem
            {
                StepNumber = x.StepNumber,
                ExpenseItemId = x.ExpenseItemId,
                PlannedValue = Domain.Metadata.FinancesDistributionItemValueType.GetValueFromStringValue(x.PlannedValue),
                ValueTypeCode = Domain.Metadata.FinancesDistributionItemValueType.GetTypeFromStringValue(x.PlannedValue).Code
            })
            .ToList();

        await applicationDbContext.FinanceDistributionPlans.AddAsync(newFinanceDistributionPlan, cancellationToken);
        await applicationDbContext.SaveChangesAsync(cancellationToken);

        response.Id = newFinanceDistributionPlan.Id;

        return response;
    }
}