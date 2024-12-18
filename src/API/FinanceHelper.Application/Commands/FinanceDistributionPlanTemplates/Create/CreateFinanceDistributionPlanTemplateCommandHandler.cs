﻿using System.Diagnostics.CodeAnalysis;
using FinanceHelper.Application.Exceptions;
using FinanceHelper.Application.Services.Localization.Interfaces;
using FinanceHelper.Domain.Entities;
using FinanceHelper.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceHelper.Application.Commands.FinanceDistributionPlanTemplates.Create;

[SuppressMessage("Performance", "CA1862")]
public class CreateFinanceDistributionPlanTemplateCommandHandler(ApplicationDbContext applicationDbContext, IStringLocalizer<CreateFinanceDistributionPlanTemplateCommandHandler> stringLocalizer) : IRequestHandler<CreateFinanceDistributionPlanTemplateCommandRequest, CreateFinanceDistributionPlanTemplateCommandResponse>
{
    public async Task<CreateFinanceDistributionPlanTemplateCommandResponse> Handle(CreateFinanceDistributionPlanTemplateCommandRequest request, CancellationToken cancellationToken)
    {
        var response = new CreateFinanceDistributionPlanTemplateCommandResponse();

        var isExists = await applicationDbContext.FinanceDistributionPlanTemplates
            .AnyAsync(x => x.Name.ToLower() == request.Name.ToLower()
                           && x.OwnerId == request.OwnerId, cancellationToken);
        if (isExists) throw new BadRequestException(stringLocalizer["AlreadyExists", request.Name]);

        var allExpenseItemsId = request.FloatingPlanItems
            .Select(x => x.Id)
            .ToList();

        if (request.FixedPlanItems is { Count: > 0 })
            allExpenseItemsId = allExpenseItemsId
                .Concat(request.FixedPlanItems.Select(y => y.Id))
                .ToList();

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
            ExpenseItemId = x.Id
        }));

        if (request.FixedPlanItems is { Count: > 0 })
            planItems.AddRange(request.FixedPlanItems.Select(x => new FinanceDistributionPlanTemplateItem
            {
                PlannedValue = x.PlannedValue,
                ValueTypeCode = x.Indivisible
                    ? FinanceHelper.Domain.Metadata.FinancesDistributionItemValueType.FixedIndivisible.Code
                    : FinanceHelper.Domain.Metadata.FinancesDistributionItemValueType.Fixed.Code,
                ExpenseItemId = x.Id
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