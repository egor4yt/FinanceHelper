using FinanceHelper.Application.Exceptions;
using FinanceHelper.Application.Services.Localization.Interfaces;
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

        var allExpenseItemsId = request.FloatingPlanItems
            .Select(x => x.ExpenseItemId)
            .ToList();

        if (request.FixedPlanItems is { Count: > 0 })
            allExpenseItemsId = allExpenseItemsId
                .Concat(request.FixedPlanItems.Select(y => y.ExpenseItemId))
                .ToList();

        var existsExpenseItemsIds = await applicationDbContext.ExpenseItems
            .Where(x => allExpenseItemsId.Contains(x.Id) && x.OwnerId == request.OwnerId)
            .Select(x => x.Id)
            .ToListAsync(cancellationToken);
        var notExistsExpenseItemsIds = allExpenseItemsId
            .Except(existsExpenseItemsIds)
            .ToList();
        if (notExistsExpenseItemsIds.Count != 0) throw new BadRequestException(stringLocalizer["ExpenseItemsDoesNotExists", string.Join(", ", notExistsExpenseItemsIds)]);

        var planItems = new List<FinanceDistributionPlanItem>();
        planItems.AddRange(request.FloatingPlanItems.Select(x => new FinanceDistributionPlanItem
        {
            PlannedValue = x.PlannedValue,
            ValueTypeCode = FinanceHelper.Domain.Metadata.FinancesDistributionItemValueType.Floating.Code,
            ExpenseItemId = x.ExpenseItemId
        }));

        if (request.FixedPlanItems is { Count: > 0 })
            planItems.AddRange(request.FixedPlanItems.Select(x => new FinanceDistributionPlanItem
            {
                PlannedValue = x.PlannedValue,
                ValueTypeCode = x.Indivisible
                    ? FinanceHelper.Domain.Metadata.FinancesDistributionItemValueType.FixedIndivisible.Code
                    : FinanceHelper.Domain.Metadata.FinancesDistributionItemValueType.Fixed.Code,
                ExpenseItemId = x.ExpenseItemId
            }));

        var newFinanceDistributionPlan = new FinanceDistributionPlan
        {
            PlannedBudget = request.PlannedBudget,
            FactBudget = request.FactBudget,
            CreatedAt = DateTime.UtcNow,
            OwnerId = request.OwnerId,
            IncomeSourceId = request.IncomeSourceId,
            FinanceDistributionPlanItems = planItems
        };

        await applicationDbContext.FinanceDistributionPlans.AddAsync(newFinanceDistributionPlan, cancellationToken);
        await applicationDbContext.SaveChangesAsync(cancellationToken);

        response.Id = newFinanceDistributionPlan.Id;

        return response;
    }
}