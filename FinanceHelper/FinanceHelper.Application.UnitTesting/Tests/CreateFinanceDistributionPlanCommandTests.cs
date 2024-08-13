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
            PlanItems =
            [
                new PlanItem
                {
                    StepNumber = 1,
                    PlannedValue = new Random().Next(),
                    ExpenseItemId = expenseItem1.Id,
                    PlannedValueTypeCode = Domain.Metadata.FinancesDistributionItemValueType.Fixed.Code
                },
                new PlanItem
                {
                    StepNumber = 2,
                    PlannedValue = new Random().Next(),
                    ExpenseItemId = expenseItem2.Id,
                    PlannedValueTypeCode = Domain.Metadata.FinancesDistributionItemValueType.FixedIndivisible.Code
                },
                new PlanItem
                {
                    StepNumber = 2,
                    PlannedValue = 100,
                    ExpenseItemId = expenseItem3.Id,
                    PlannedValueTypeCode = Domain.Metadata.FinancesDistributionItemValueType.Floating.Code
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
        planItemsAsserts.Add(() => Assert.Equal(request.PlanItems.Count, plan!.FinanceDistributionPlanItems.Count));

        foreach (var requestPlanItem in request.PlanItems)
        {
            var storedPlanItems = plan?.FinanceDistributionPlanItems
                .Where(x => x.StepNumber == requestPlanItem.StepNumber
                            && x.PlannedValue == requestPlanItem.PlannedValue
                            && x.ExpenseItem.Id == requestPlanItem.ExpenseItemId
                            && x.ValueType.Code == requestPlanItem.PlannedValueTypeCode)
                .ToList();

            planItemsAsserts.Add(() => Assert.True(storedPlanItems?.Count == 1, "Plan item exception"));
        }

        Assert.Multiple(planItemsAsserts.ToArray());
    }

    [Fact]
    public async Task Success_NewExpenseItem()
    {
        // Arrange
        var incomeSource = await ApplicationDbContext.SeedOneIncomeSourceAsync();
        var request = new CreateFinanceDistributionPlanCommandRequest
        {
            OwnerId = incomeSource.OwnerId,
            PlannedBudget = new Random().Next(),
            FactBudget = new Random().Next(),
            IncomeSourceId = incomeSource.Id,
            PlanItems =
            [
                new PlanItem
                {
                    StepNumber = 1,
                    PlannedValue = 100,
                    ExpenseItemId = null,
                    NewExpenseItemName = Guid.NewGuid().ToString(),
                    PlannedValueTypeCode = Domain.Metadata.FinancesDistributionItemValueType.Floating.Code
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
                            && x.PlannedValue == requestPlanItem.PlannedValue
                            && x.ExpenseItem.Name == requestPlanItem.NewExpenseItemName
                            && x.ValueType.Code == requestPlanItem.PlannedValueTypeCode)
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
                    PlannedValueTypeCode = Domain.Metadata.FinancesDistributionItemValueType.Fixed.Code
                },
                new PlanItem
                {
                    StepNumber = 2,
                    PlannedValue = 50,
                    ExpenseItemId = 1,
                    PlannedValueTypeCode = Domain.Metadata.FinancesDistributionItemValueType.Floating.Code
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
                    PlannedValueTypeCode = Domain.Metadata.FinancesDistributionItemValueType.Fixed.Code
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
        var expenseItem1 = await ApplicationDbContext.SeedOneExpenseItemAsync();
        var expenseItem2 = await ApplicationDbContext.SeedOneExpenseItemAsync();
        var request = new CreateFinanceDistributionPlanCommandRequest
        {
            OwnerId = expenseItem1.OwnerId,
            PlannedBudget = new Random().Next(),
            FactBudget = new Random().Next(),
            PlanItems =
            [
                new PlanItem
                {
                    StepNumber = new Random().Next(),
                    PlannedValue = new Random().Next(),
                    ExpenseItemId = expenseItem1.Id,
                    PlannedValueTypeCode = Domain.Metadata.FinancesDistributionItemValueType.Fixed.Code
                },
                new PlanItem
                {
                    StepNumber = new Random().Next(),
                    PlannedValue = new Random().Next(),
                    ExpenseItemId = expenseItem2.Id,
                    PlannedValueTypeCode = Domain.Metadata.FinancesDistributionItemValueType.Fixed.Code
                }
            ]
        };

        // Assert
        await Assert.ThrowsAsync<BadRequestException>(async () => await _handler.Handle(request, CancellationToken.None));
    }
}