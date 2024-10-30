using FinanceHelper.Application.Commands.ExpenseItems.Delete;
using FinanceHelper.Application.Exceptions;
using FinanceHelper.Application.UnitTesting.Common;
using FinanceHelper.Application.UnitTesting.Generators;
using Microsoft.EntityFrameworkCore;

namespace FinanceHelper.Application.UnitTesting.Tests;

public class DeleteExpenseItemCommandTests : TestBase<DeleteExpenseItemCommandHandler>
{
    private readonly DeleteExpenseItemCommandHandler _handler;

    public DeleteExpenseItemCommandTests()
    {
        _handler = new DeleteExpenseItemCommandHandler(ApplicationDbContext, Localizer);
    }

    [Fact]
    public async Task Success()
    {
        // Arrange
        var expenseItem = await ApplicationDbContext.SeedOneExpenseItemAsync();
        var request = new DeleteExpenseItemCommandRequest
        {
            OwnerId = expenseItem.OwnerId,
            ExpenseItemId = expenseItem.Id
        };

        // Act
        await _handler.Handle(request, CancellationToken.None);

        var databaseObject = await ApplicationDbContext.ExpenseItems
            .SingleOrDefaultAsync(x => x.Id == expenseItem.Id);

        // Assert
        Assert.Null(databaseObject);
    }

    [Fact]
    public async Task InvalidExpenseItemOwner()
    {
        // Arrange
        var expenseItem1 = await ApplicationDbContext.SeedOneExpenseItemAsync();
        var expenseItem2 = await ApplicationDbContext.SeedOneExpenseItemAsync();
        var request = new DeleteExpenseItemCommandRequest
        {
            OwnerId = expenseItem1.OwnerId,
            ExpenseItemId = expenseItem2.Id
        };

        // Assert
        await Assert.ThrowsAsync<NotFoundException>(async () => await _handler.Handle(request, CancellationToken.None));
    }

    [Fact]
    public async Task InvalidExpenseItemId()
    {
        // Arrange
        var expenseItem = await ApplicationDbContext.SeedOneExpenseItemAsync();
        var request = new DeleteExpenseItemCommandRequest
        {
            OwnerId = expenseItem.OwnerId,
            ExpenseItemId = -1
        };

        // Assert
        await Assert.ThrowsAsync<NotFoundException>(async () => await _handler.Handle(request, CancellationToken.None));
    }

    [Fact]
    public async Task ExpenseItemWithPlans()
    {
        // Arrange
        var expenseItem = await ApplicationDbContext.SeedOneExpenseItemAsync();
        var plan = await ApplicationDbContext.SeedOneFinanceDistributionPlanAsync();
        var planItem = await ApplicationDbContext.SeedOneFinanceDistributionPlanItemAsync(plan, expenseItem);
        var request = new DeleteExpenseItemCommandRequest
        {
            OwnerId = expenseItem.OwnerId,
            ExpenseItemId = expenseItem.OwnerId
        };

        // Act
        await _handler.Handle(request, CancellationToken.None);

        var databaseObject = await ApplicationDbContext.ExpenseItems
            .SingleAsync(x => x.Id == expenseItem.Id
                              && x.OwnerId == expenseItem.OwnerId);

        // Assert
        Assert.NotNull(databaseObject.DeletedAt);
    }
}