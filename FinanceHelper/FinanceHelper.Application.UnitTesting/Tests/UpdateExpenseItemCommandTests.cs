using FinanceHelper.Application.Commands.ExpenseItems.Update;
using FinanceHelper.Application.Exceptions;
using FinanceHelper.Application.UnitTesting.Common;
using FinanceHelper.Application.UnitTesting.Generators;
using Microsoft.EntityFrameworkCore;

namespace FinanceHelper.Application.UnitTesting.Tests;

public class UpdateExpenseItemCommandTests : TestBase<UpdateExpenseItemCommandHandler>
{
    private readonly UpdateExpenseItemCommandHandler _handler;

    public UpdateExpenseItemCommandTests()
    {
        _handler = new UpdateExpenseItemCommandHandler(ApplicationDbContext, Localizer);
    }

    [Fact]
    public async Task Success()
    {
        // Arrange
        var expenseItem = await ApplicationDbContext.SeedOneExpenseItemAsync();
        var expenseItemType = await ApplicationDbContext.SeedOneExpenseItemTypeAsync();
        var request = new UpdateExpenseItemCommandRequest
        {
            OwnerId = expenseItem.OwnerId,
            ExpenseItemId = expenseItem.Id,
            Color = Guid.NewGuid().ToString(),
            Name = Guid.NewGuid().ToString(),
            ExpenseItemTypeCode = expenseItemType.Code
        };

        // Act
        await _handler.Handle(request, CancellationToken.None);

        var databaseObject = await ApplicationDbContext.ExpenseItems
            .SingleOrDefaultAsync(x => x.Id == expenseItem.Id
                                       && x.OwnerId == expenseItem.OwnerId
                                       && x.ExpenseItemTypeCode == expenseItemType.Code
                                       && x.Color == request.Color
                                       && x.Name == request.Name);

        // Assert
        Assert.NotNull(databaseObject);
    }

    [Fact]
    public async Task InvalidExpenseItemOwner()
    {
        // Arrange
        var expenseItem = await ApplicationDbContext.SeedOneExpenseItemAsync();
        var expenseItemType = await ApplicationDbContext.SeedOneExpenseItemTypeAsync();
        var request = new UpdateExpenseItemCommandRequest
        {
            OwnerId = -1,
            ExpenseItemId = expenseItem.Id,
            Color = Guid.NewGuid().ToString(),
            Name = Guid.NewGuid().ToString(),
            ExpenseItemTypeCode = expenseItemType.Code
        };

        // Assert
        await Assert.ThrowsAsync<NotFoundException>(async () => await _handler.Handle(request, CancellationToken.None));
    }

    [Fact]
    public async Task InvalidExpenseItemId()
    {
        // Arrange
        var expenseItem = await ApplicationDbContext.SeedOneExpenseItemAsync();
        var expenseItemType = await ApplicationDbContext.SeedOneExpenseItemTypeAsync();
        var request = new UpdateExpenseItemCommandRequest
        {
            OwnerId = expenseItem.OwnerId,
            ExpenseItemId = -1,
            Color = Guid.NewGuid().ToString(),
            Name = Guid.NewGuid().ToString(),
            ExpenseItemTypeCode = expenseItemType.Code
        };

        // Assert
        await Assert.ThrowsAsync<NotFoundException>(async () => await _handler.Handle(request, CancellationToken.None));
    }

    [Fact]
    public async Task InvalidExpenseItemType()
    {
        // Arrange
        var expenseItem = await ApplicationDbContext.SeedOneExpenseItemAsync();
        var request = new UpdateExpenseItemCommandRequest
        {
            OwnerId = expenseItem.OwnerId,
            ExpenseItemId = expenseItem.Id,
            Color = Guid.NewGuid().ToString(),
            Name = Guid.NewGuid().ToString(),
            ExpenseItemTypeCode = Guid.NewGuid().ToString()
        };

        // Assert
        await Assert.ThrowsAsync<BadRequestException>(async () => await _handler.Handle(request, CancellationToken.None));
    }
}