using System.Diagnostics.CodeAnalysis;
using FinanceHelper.Application.Exceptions;
using FinanceHelper.Application.Services.Localization.Interfaces;
using FinanceHelper.Domain.Entities;
using FinanceHelper.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceHelper.Application.Commands.ExpenseItems.Create;

[SuppressMessage("Performance", "CA1862")]
public class CreateExpenseItemCommandHandler(ApplicationDbContext applicationDbContext, IStringLocalizer<CreateExpenseItemCommandHandler> stringLocalizer) : IRequestHandler<CreateExpenseItemCommandRequest, CreateExpenseItemCommandResponse>
{
    public async Task<CreateExpenseItemCommandResponse> Handle(CreateExpenseItemCommandRequest request, CancellationToken cancellationToken)
    {
        var response = new CreateExpenseItemCommandResponse();

        var isValidExpenseItemTypeCode = await applicationDbContext.ExpenseItemTypes.AnyAsync(x => x.Code == request.ExpenseItemTypeCode, cancellationToken);
        if (isValidExpenseItemTypeCode == false) throw new BadRequestException(stringLocalizer["ExpenseItemTypeDoesNotExists", request.ExpenseItemTypeCode]);

        var expenseItemIsExists = await applicationDbContext.ExpenseItems
            .AnyAsync(x => x.Name.ToLower() == request.Name.ToLower()
                           && x.OwnerId == request.OwnerId, cancellationToken);
        if (expenseItemIsExists) throw new BadRequestException(stringLocalizer["ExpenseItemExists", request.Name]);

        var newExpenseItem = new ExpenseItem
        {
            Name = request.Name,
            ExpenseItemTypeCode = request.ExpenseItemTypeCode,
            Color = request.Color,
            OwnerId = request.OwnerId
        };

        await applicationDbContext.ExpenseItems.AddAsync(newExpenseItem, cancellationToken);
        await applicationDbContext.SaveChangesAsync(cancellationToken);

        response.Id = newExpenseItem.Id;

        return response;
    }
}