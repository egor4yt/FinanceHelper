using FinanceHelper.Application.Commands.ExpenseItems.Create;
using FinanceHelper.Application.Exceptions;
using FinanceHelper.Application.UnitTesting.Common;
using Microsoft.EntityFrameworkCore;

namespace FinanceHelper.Application.UnitTesting.Tests;

public class CreateExpenseItemCommandTests : TestBase<CreateExpenseItemCommandHandler>
{
    private readonly CreateExpenseItemCommandHandler _handler;

    public CreateExpenseItemCommandTests()
    {
        _handler = new CreateExpenseItemCommandHandler(ApplicationDbContext, Localizer);
    }

    [Fact]
    public async Task Success()
    {
        // Arrange
        var expenseType = await ExpenseItemTypeGenerator.SeedOneAsync();
        var owner = await UserGenerator.SeedOneAsync();
        var request = new CreateExpenseItemCommandRequest
        {
            OwnerId = owner.Id,
            Name = Guid.NewGuid().ToString(),
            Color = Guid.NewGuid().ToString(),
            ExpenseItemTypeCode = expenseType.Code
        };

        // Act
        var response = await _handler.Handle(request, CancellationToken.None);

        var databaseObject = await ApplicationDbContext.ExpenseItems
            .SingleOrDefaultAsync(x => x.Id == response.Id
                                       && x.ExpenseItemType.Code == request.ExpenseItemTypeCode
                                       && x.Color == request.Color
                                       && x.Owner.Id == request.OwnerId
                                       && x.Name == request.Name
                                       && x.OneTimeUsable == false);

        // Assert
        Assert.NotNull(databaseObject);
    }

    [Fact]
    public async Task InvalidExpenseItemTypeCode()
    {
        // Arrange
        var request = new CreateExpenseItemCommandRequest
        {
            OwnerId = new Random().Next(),
            Name = Guid.NewGuid().ToString(),
            Color = Guid.NewGuid().ToString(),
            ExpenseItemTypeCode = Guid.NewGuid().ToString()
        };

        // Assert
        await Assert.ThrowsAsync<BadRequestException>(async () => await _handler.Handle(request, CancellationToken.None));
    }
}