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
            CreationDate = DateTime.UtcNow,
            OwnerId = request.OwnerId
        };
        newFinanceDistributionPlan.FinanceDistributionPlanItems = request.PlanItems
            .Select(x => new FinanceDistributionPlanItem
            {
                StepNumber = x.StepNumber,
                PlannedValue = x.PlannedValue,
                ExpenseItemId = x.ExpenseItemId,
                ValueTypeCode = x.ValueTypeCode
            })
            .ToList();

        await applicationDbContext.FinanceDistributionPlans.AddAsync(newFinanceDistributionPlan, cancellationToken);
        await applicationDbContext.SaveChangesAsync(cancellationToken);

        response.Id = newFinanceDistributionPlan.Id;

        return response;
    }
}