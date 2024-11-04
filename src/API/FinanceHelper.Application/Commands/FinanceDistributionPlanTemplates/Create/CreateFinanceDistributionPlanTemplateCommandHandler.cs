using FinanceHelper.Application.Exceptions;
using FinanceHelper.Application.Services.Localization.Interfaces;
using FinanceHelper.Domain.Entities;
using FinanceHelper.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceHelper.Application.Commands.FinanceDistributionPlanTemplates.Create;

public class CreateFinanceDistributionPlanTemplateCommandHandler(ApplicationDbContext applicationDbContext, IStringLocalizer<CreateFinanceDistributionPlanTemplateCommandHandler> stringLocalizer) : IRequestHandler<CreateFinanceDistributionPlanTemplateCommandRequest, CreateFinanceDistributionPlanTemplateCommandResponse>
{
    public async Task<CreateFinanceDistributionPlanTemplateCommandResponse> Handle(CreateFinanceDistributionPlanTemplateCommandRequest request, CancellationToken cancellationToken)
    {
        var response = new CreateFinanceDistributionPlanTemplateCommandResponse();

        var allExpenseItemsId = request.FixedPlanItems
            .Select(x => x.ExpenseItemId)
            .Concat(request.FloatingPlanItems.Select(x => x.ExpenseItemId))
            .ToList();
        var duplicatedExpenseItemsIds = allExpenseItemsId
            .GroupBy(x => x)
            .Where(x => x.Count() > 1)
            .Select(x => x.Key)
            .ToList();
        if (duplicatedExpenseItemsIds.Count != 0) throw new BadRequestException(stringLocalizer["DuplicatedExpenseItems", string.Join(", ", duplicatedExpenseItemsIds)]);

        var sumFloatedValues = request.FloatingPlanItems
            .Sum(plaItem => plaItem.PlannedValue);

        if (sumFloatedValues != 100) throw new BadRequestException(stringLocalizer["InvalidFloatingValue"]);

        var existsExpenseItemsIds = await applicationDbContext.ExpenseItems
            .Where(x => allExpenseItemsId.Contains(x.Id) && x.OwnerId == request.OwnerId)
            .Select(x => x.Id)
            .ToListAsync(cancellationToken);
        var notExistsExpenseItemsIds = allExpenseItemsId
            .Except(existsExpenseItemsIds)
            .ToList();
        if (notExistsExpenseItemsIds.Count != 0) throw new BadRequestException(stringLocalizer["ExpenseItemsDoesNotExists", string.Join(", ", notExistsExpenseItemsIds)]);

        var planItems = new List<FinanceDistributionPlanTemplateItem>();
        planItems.AddRange(request.FloatingPlanItems.Select(x => new FinanceDistributionPlanTemplateItem
        {
            PlannedValue = x.PlannedValue,
            ValueTypeCode = FinanceHelper.Domain.Metadata.FinancesDistributionItemValueType.Floating.Code,
            ExpenseItemId = x.ExpenseItemId
        }));

        if (request.FixedPlanItems.Count != 0)
            planItems.AddRange(request.FixedPlanItems.Select(x => new FinanceDistributionPlanTemplateItem
            {
                PlannedValue = x.PlannedValue,
                ValueTypeCode = x.Indivisible
                    ? FinanceHelper.Domain.Metadata.FinancesDistributionItemValueType.FixedIndivisible.Code
                    : FinanceHelper.Domain.Metadata.FinancesDistributionItemValueType.Fixed.Code,
                ExpenseItemId = x.ExpenseItemId
            }));

        var newFinanceDistributionPlanTemplate = new FinanceDistributionPlanTemplate
        {
            PlannedBudget = request.PlannedBudget,
            OwnerId = request.OwnerId,
            IncomeSourceId = request.IncomeSourceId,
            FinanceDistributionPlanTemplateItems = planItems,
            Name = request.Name
        };

        await applicationDbContext.FinanceDistributionPlanTemplates.AddAsync(newFinanceDistributionPlanTemplate, cancellationToken);
        await applicationDbContext.SaveChangesAsync(cancellationToken);

        response.Id = newFinanceDistributionPlanTemplate.Id;

        return response;
    }
}