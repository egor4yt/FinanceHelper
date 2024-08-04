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
        var owner = await UserGenerator.SeedOneAsync();
        var expenseItem1 = await ExpenseItemGenerator.SeedOneAsync(owner);
        var expenseItem2 = await ExpenseItemGenerator.SeedOneAsync(owner);
        var incomeSource = await IncomeSourceGenerator.SeedOneAsync(owner);
        var request = new CreateFinanceDistributionPlanCommandRequest
        {
            OwnerId = owner.Id,
            PlannedBudget = new Random().Next(),
            FactBudget = new Random().Next(),
            IncomeSourceId = incomeSource.Id,
            PlanItems =
            [
                new PlanItem
                {
                    StepNumber = 1,
                    PlannedValue = new Random().Next().ToString(),
                    ExpenseItemId = expenseItem1.Id
                },
                new PlanItem
                {
                    StepNumber = 2,
                    PlannedValue = "100%",
                    ExpenseItemId = expenseItem2.Id
                }
            ]
        };

        // Act
        var response = await _handler.Handle(request, CancellationToken.None);

        var plan = await ApplicationDbContext.FinanceDistributionPlans
            .Include(x => x.FinanceDistributionPlanItems).ThenInclude(x => x.ExpenseItem)
            .Include(x => x.FinanceDistributionPlanItems).ThenInclude(financeDistributionPlanItem => financeDistributionPlanItem.ValueType)
            .SingleOrDefaultAsync(x => x.Id == response.Id
                                       && x.Owner.Id == request.OwnerId
                                       && x.PlannedBudget == request.PlannedBudget
                                       && x.FactBudget == request.FactBudget
                                       && x.IncomeSource.Id == request.IncomeSourceId);

        // Assert
        var planItemsAsserts = new List<Action>();
        planItemsAsserts.Add(() => Assert.NotNull(plan));
        planItemsAsserts.Add(() => Assert.Equal(request.PlanItems.Count, plan!.FinanceDistributionPlanItems.Count));

        foreach (var requestPlanItem in request.PlanItems)
        {
            var storedPlanItems = plan?.FinanceDistributionPlanItems
                .Where(x => x.StepNumber == requestPlanItem.StepNumber
                            && x.PlannedValue == Domain.Metadata.FinancesDistributionItemValueType.GetValueFromStringValue(requestPlanItem.PlannedValue)
                            && x.ExpenseItem.Id == requestPlanItem.ExpenseItemId
                            && x.ValueType.Code == Domain.Metadata.FinancesDistributionItemValueType.GetTypeFromStringValue(requestPlanItem.PlannedValue).Code)
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
                    PlannedValue = "1000",
                    ExpenseItemId = 1
                },
                new PlanItem
                {
                    StepNumber = 2,
                    PlannedValue = "50%",
                    ExpenseItemId = 1
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
                    PlannedValue = new Random().Next().ToString(),
                    ExpenseItemId = new Random().Next()
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
                    PlannedValue = new Random().Next().ToString(),
                    ExpenseItemId = userExpenseItem.Id
                },
                new PlanItem
                {
                    StepNumber = new Random().Next(),
                    PlannedValue = new Random().Next().ToString(),
                    ExpenseItemId = notUserExpenseItem.Id
                }
            ]
        };

        // Assert
        await Assert.ThrowsAsync<BadRequestException>(async () => await _handler.Handle(request, CancellationToken.None));
    }
}