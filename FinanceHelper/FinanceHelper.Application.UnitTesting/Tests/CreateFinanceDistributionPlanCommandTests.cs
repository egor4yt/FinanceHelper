using FinanceHelper.Application.Commands.FinanceDistributionPlans.Create;
using FinanceHelper.Application.Exceptions;
using FinanceHelper.Application.UnitTesting.Common;
using FinanceHelper.Application.UnitTesting.Generators;
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
        var owner = await ApplicationDbContext.SeedOneUserAsync();
        var expenseItem1 = await ApplicationDbContext.SeedOneExpenseItemAsync(owner);
        var expenseItem2 = await ApplicationDbContext.SeedOneExpenseItemAsync(owner);
        var expenseItem3 = await ApplicationDbContext.SeedOneExpenseItemAsync(owner);
        var incomeSource = await ApplicationDbContext.SeedOneIncomeSourceAsync(owner);
        var request = new CreateFinanceDistributionPlanCommandRequest
        {
            OwnerId = owner.Id,
            PlannedBudget = new Random().Next(),
            FactBudget = new Random().Next(),
            IncomeSourceId = incomeSource.Id,
            FixedPlanItems =
            [
                new FixedPlanItem
                {
                    PlannedValue = new Random().Next(),
                    ExpenseItemId = expenseItem1.Id,
                    Indivisible = false
                },
                new FixedPlanItem
                {
                    PlannedValue = new Random().Next(),
                    ExpenseItemId = expenseItem2.Id,
                    Indivisible = false
                }
            ],
            FloatingPlanItems =
            [
                new FloatingPlanItem
                {
                    PlannedValue = 100,
                    ExpenseItemId = expenseItem3.Id
                }
            ]
        };

        // Act
        var response = await _handler.Handle(request, CancellationToken.None);

        var plan = await ApplicationDbContext.FinanceDistributionPlans
            .Include(x => x.FinanceDistributionPlanItems).ThenInclude(x => x.ExpenseItem)
            .Include(x => x.FinanceDistributionPlanItems).ThenInclude(x => x.ValueType)
            .SingleOrDefaultAsync(x => x.Id == response.Id
                                       && x.Owner.Id == request.OwnerId
                                       && x.PlannedBudget == request.PlannedBudget
                                       && x.FactBudget == request.FactBudget
                                       && x.IncomeSource.Id == request.IncomeSourceId);

        // Assert
        var planItemsAsserts = new List<Action>();
        planItemsAsserts.Add(() => Assert.NotNull(plan));
        planItemsAsserts.Add(() => Assert.Equal(request.FixedPlanItems.Count + request.FloatingPlanItems.Count, plan!.FinanceDistributionPlanItems.Count));

        foreach (var requestPlanItem in request.FixedPlanItems)
        {
            var type = FinanceHelper.Domain.Metadata.FinancesDistributionItemValueType.Fixed.Code;
            if (requestPlanItem.Indivisible) type = FinanceHelper.Domain.Metadata.FinancesDistributionItemValueType.FixedIndivisible.Code;

            var storedPlanItems = plan?.FinanceDistributionPlanItems
                .Where(x => x.PlannedValue == requestPlanItem.PlannedValue
                            && x.ExpenseItem.Id == requestPlanItem.ExpenseItemId
                            && x.ValueType.Code == type)
                .ToList();

            planItemsAsserts.Add(() => Assert.True(storedPlanItems?.Count == 1, "Fixed plan item exception"));
        }

        foreach (var requestPlanItem in request.FloatingPlanItems)
        {
            var storedPlanItems = plan?.FinanceDistributionPlanItems
                .Where(x => x.PlannedValue == requestPlanItem.PlannedValue
                            && x.ExpenseItem.Id == requestPlanItem.ExpenseItemId
                            && x.ValueType.Code == FinanceHelper.Domain.Metadata.FinancesDistributionItemValueType.Floating.Code)
                .ToList();

            planItemsAsserts.Add(() => Assert.True(storedPlanItems?.Count == 1, "Floating plan item exception"));
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
            FixedPlanItems =
            [
                new FixedPlanItem
                {
                    PlannedValue = 1000,
                    ExpenseItemId = 1,
                    Indivisible = false
                }
            ],
            FloatingPlanItems =
            [
                new FloatingPlanItem
                {
                    PlannedValue = 50,
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
            FloatingPlanItems =
            [
                new FloatingPlanItem
                {
                    PlannedValue = 50,
                    ExpenseItemId = -1
                }
            ],
            FixedPlanItems = []
        };

        // Assert
        await Assert.ThrowsAsync<BadRequestException>(async () => await _handler.Handle(request, CancellationToken.None));
    }

    [Fact]
    public async Task OtherUserExpenseItem()
    {
        // Arrange
        var expenseItem1 = await ApplicationDbContext.SeedOneExpenseItemAsync();
        var expenseItem2 = await ApplicationDbContext.SeedOneExpenseItemAsync();
        var request = new CreateFinanceDistributionPlanCommandRequest
        {
            OwnerId = expenseItem1.OwnerId,
            PlannedBudget = new Random().Next(),
            FactBudget = new Random().Next(),
            FixedPlanItems =
            [
                new FixedPlanItem
                {
                    PlannedValue = new Random().Next(),
                    ExpenseItemId = expenseItem1.Id,
                    Indivisible = false
                }
            ],
            FloatingPlanItems =
            [
                new FloatingPlanItem
                {
                    PlannedValue = new Random().Next(),
                    ExpenseItemId = expenseItem2.Id
                }
            ]
        };

        // Assert
        await Assert.ThrowsAsync<BadRequestException>(async () => await _handler.Handle(request, CancellationToken.None));
    }
}