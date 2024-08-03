using FinanceHelper.Application.Commands.FinanceDistributionPlans.Create;
using FinanceHelper.Application.Exceptions;
using FinanceHelper.Application.UnitTesting.Common;
using Microsoft.EntityFrameworkCore;

namespace FinanceHelper.Application.UnitTesting.Tests;

public class CreateFinanceDistributionPlanCommandTests : TestBase<CreateFinanceDistributionPlanCommandHandler>
{
    private readonly CreateFinanceDistributionPlanCommandHandler _handler;

    public CreateFinanceDistributionPlanCommandTests()
    {
        _handler = new CreateFinanceDistributionPlanCommandHandler(ApplicationDbContext, Localizer);
    }

    [Fact]
    public async Task Success()
    {
        // Arrange
        var user = await UserGenerator.SeedOneAsync();
        var expenseItem1 = await ExpenseItemGenerator.SeedOneAsync(user);
        var expenseItem2 = await ExpenseItemGenerator.SeedOneAsync(user);
        var request = new CreateFinanceDistributionPlanCommandRequest
        {
            OwnerId = user.Id,
            PlannedBudget = new Random().Next(),
            FactBudget = new Random().Next(),
            PlanItems =
            [
                new PlanItem
                {
                    StepNumber = new Random().Next(),
                    PlannedValue = new Random().Next(),
                    ExpenseItemId = expenseItem1.Id,
                    ValueTypeCode = Guid.NewGuid().ToString()
                },
                new PlanItem
                {
                    StepNumber = new Random().Next(),
                    PlannedValue = new Random().Next(),
                    ExpenseItemId = expenseItem2.Id,
                    ValueTypeCode = Guid.NewGuid().ToString()
                }
            ]
        };

        // Act
        var response = await _handler.Handle(request, CancellationToken.None);

        var plan = await ApplicationDbContext.FinanceDistributionPlans
            .Include(x => x.FinanceDistributionPlanItems)
            .SingleOrDefaultAsync(x => x.Id == response.Id
                                       && x.OwnerId == request.OwnerId
                                       && x.PlannedBudget == request.PlannedBudget
                                       && x.FactBudget == request.FactBudget);

        // Assert
        var planItemsAsserts = new List<Action>();
        planItemsAsserts.Add(() => Assert.NotNull(plan));
        planItemsAsserts.Add(() => Assert.Equal(request.PlanItems.Count, plan!.FinanceDistributionPlanItems.Count));

        foreach (var requestPlanItem in request.PlanItems)
        {
            var storedPlanItems = plan?.FinanceDistributionPlanItems
                .Where(x => x.StepNumber == requestPlanItem.StepNumber
                            && x.PlannedValue == requestPlanItem.PlannedValue
                            && x.ExpenseItemId == requestPlanItem.ExpenseItemId
                            && x.ValueTypeCode == requestPlanItem.ValueTypeCode)
                .ToList();

            planItemsAsserts.Add(() => Assert.True(storedPlanItems?.Count == 1, "Plan item exception"));
        }

        Assert.Multiple(planItemsAsserts.ToArray());
    }

    [Fact]
    public async Task DuplicatedExpenseItems()
    {
        // Arrange
        var request = new CreateFinanceDistributionPlanCommandRequest
        {
            OwnerId = new Random().Next(),
            PlannedBudget = new Random().Next(),
            FactBudget = new Random().Next(),
            PlanItems =
            [
                new PlanItem
                {
                    StepNumber = 1,
                    PlannedValue = 1000,
                    ExpenseItemId = 1,
                    ValueTypeCode = Guid.NewGuid().ToString()
                },
                new PlanItem
                {
                    StepNumber = 2,
                    PlannedValue = 2000,
                    ExpenseItemId = 1,
                    ValueTypeCode = Guid.NewGuid().ToString()
                }
            ]
        };

        // Assert
        await Assert.ThrowsAsync<BadRequestException>(async () => await _handler.Handle(request, CancellationToken.None));
    }

    [Fact]
    public async Task NotExistsExpenseItem()
    {
        // Arrange
        var request = new CreateFinanceDistributionPlanCommandRequest
        {
            OwnerId = new Random().Next(),
            PlannedBudget = new Random().Next(),
            FactBudget = new Random().Next(),
            PlanItems =
            [
                new PlanItem
                {
                    StepNumber = new Random().Next(),
                    PlannedValue = new Random().Next(),
                    ExpenseItemId = new Random().Next(),
                    ValueTypeCode = Guid.NewGuid().ToString()
                }
            ]
        };

        // Assert
        await Assert.ThrowsAsync<BadRequestException>(async () => await _handler.Handle(request, CancellationToken.None));
    }

    [Fact]
    public async Task OtherUserExpenseItem()
    {
        // Arrange
        var user = await UserGenerator.SeedOneAsync();
        var userExpenseItem = await ExpenseItemGenerator.SeedOneAsync();
        var notUserExpenseItem = await ExpenseItemGenerator.SeedOneAsync(user);
        var request = new CreateFinanceDistributionPlanCommandRequest
        {
            OwnerId = new Random().Next(),
            PlannedBudget = new Random().Next(),
            FactBudget = new Random().Next(),
            PlanItems =
            [
                new PlanItem
                {
                    StepNumber = new Random().Next(),
                    PlannedValue = new Random().Next(),
                    ExpenseItemId = userExpenseItem.Id,
                    ValueTypeCode = Guid.NewGuid().ToString()
                },
                new PlanItem
                {
                    StepNumber = new Random().Next(),
                    PlannedValue = new Random().Next(),
                    ExpenseItemId = notUserExpenseItem.Id,
                    ValueTypeCode = Guid.NewGuid().ToString()
                }
            ]
        };

        // Assert
        await Assert.ThrowsAsync<BadRequestException>(async () => await _handler.Handle(request, CancellationToken.None));
    }
}