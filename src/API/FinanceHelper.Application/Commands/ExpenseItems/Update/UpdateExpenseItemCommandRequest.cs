﻿using MediatR;

namespace FinanceHelper.Application.Commands.ExpenseItems.Update;

public class UpdateExpenseItemCommandRequest : IRequest
{
    public required string Name { get; init; }
    public required string Color { get; init; }
    public required string ExpenseItemTypeCode { get; init; }
    public long ExpenseItemId { get; init; }
    public long OwnerId { get; init; }
}