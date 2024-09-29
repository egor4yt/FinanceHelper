using FinanceHelper.Application.Queries.FinanceDistributionPlans.Details;
using FinanceHelper.Application.UnitTesting.Common;
using FinanceHelper.Application.UnitTesting.Generators;
using FinanceHelper.Domain.Entities;

namespace FinanceHelper.Application.UnitTesting.Tests;

public class DetailsFinanceDistributionPlanQueryTests : TestBase<DetailsFinanceDistributionPlanQueryHandler>
{
    private readonly DetailsFinanceDistributionPlanQueryHandler _handler;

    public DetailsFinanceDistributionPlanQueryTests()
    {
        _handler = new DetailsFinanceDistributionPlanQueryHandler(ApplicationDbContext, Localizer);
    }

    [Fact]
    public async Task Success()
    {
        // Arrange
        var owner = await ApplicationDbContext.SeedOneUserAsync();
        var incomeSource = await ApplicationDbContext.SeedOneIncomeSourceAsync(owner);
        var expenseItems = await ApplicationDbContext.SeedManyExpenseItemAsync(4, owner);
        var expenseItemTag = await ApplicationDbContext.SeedOneTagAsync(Domain.Metadata.TagType.ExpenseItem.Code, owner);
        var expenseItemsWithTag = await ApplicationDbContext.SeedManyExpenseItemAsync(2, owner, null, new[] { expenseItemTag });

        var plan = new FinanceDistributionPlan
        {
            Id = 0,
            PlannedBudget = 40_000,
            FactBudget = 26_000, // Factor = 26 000 / 40 000 = 0.65
            CreatedAt = DateTime.UtcNow,
            OwnerId = owner.Id,
            IncomeSourceId = incomeSource.Id,
            FinanceDistributionPlanItems = new List<FinanceDistributionPlanItem>
            {
                new FinanceDistributionPlanItem
                {
                    PlannedValue = 10_000, // FACT = 6 500
                    ExpenseItemId = expenseItems[0].Id,
                    ValueTypeCode = Domain.Metadata.FinancesDistributionItemValueType.Fixed.Code
                },
                new FinanceDistributionPlanItem
                {
                    PlannedValue = 11, // FACT =  (26 000 - 14 150) * 0.11 = 1 303,5
                    ExpenseItemId = expenseItems[1].Id,
                    ValueTypeCode = Domain.Metadata.FinancesDistributionItemValueType.Floating.Code
                },
                new FinanceDistributionPlanItem
                {
                    PlannedValue = 17, // FACT = (26 000 - 14 150) * 0.17 = 2 014.5
                    ExpenseItemId = expenseItems[2].Id,
                    ValueTypeCode = Domain.Metadata.FinancesDistributionItemValueType.Floating.Code
                },
                new FinanceDistributionPlanItem
                {
                    PlannedValue = 72, // FACT = (26 000 - 14 150) * 0.72 = 8 532
                    ExpenseItemId = expenseItems[3].Id,
                    ValueTypeCode = Domain.Metadata.FinancesDistributionItemValueType.Floating.Code
                },
                new FinanceDistributionPlanItem
                {
                    PlannedValue = 7_000, // FACT = 7 000
                    ExpenseItemId = expenseItemsWithTag[0].Id,
                    ValueTypeCode = Domain.Metadata.FinancesDistributionItemValueType.FixedIndivisible.Code
                },
                new FinanceDistributionPlanItem
                {
                    PlannedValue = 1_000, // FACT = 1 000 * 0.65 = 650
                    ExpenseItemId = expenseItemsWithTag[1].Id,
                    ValueTypeCode = Domain.Metadata.FinancesDistributionItemValueType.Fixed.Code
                }
            }
        };

        await ApplicationDbContext.AddAsync(plan);
        await ApplicationDbContext.SaveChangesAsync();

        var expectedResponse = new DetailsFinanceDistributionPlanQueryResponse
        {
            PlannedBudget = plan.PlannedBudget,
            FactBudget = plan.FactBudget,
            CreatedAt = plan.CreatedAt,
            IncomeSource = new Queries.FinanceDistributionPlans.Details.DetailsFinanceDistributionPlanQueryResponseIncomeSource
            {
                Id = incomeSource.Id,
                Name = incomeSource.Name
            },
            TagsSum = new Dictionary<string, string>
            {
                { expenseItemTag.Name, "7 650.00" }
            },
            Items =
            [
                new DetailsFinanceDistributionPlanQueryResponseItem
                {
                    PlannedValue = "10 000.00",
                    FactFixedValue = "6 500.00",
                    ExpenseItem = new Queries.FinanceDistributionPlans.Details.ExpenseItem
                    {
                        Id = expenseItems[0].Id,
                        Name = expenseItems[0].Name,
                        Tags = []
                    }
                },
                new DetailsFinanceDistributionPlanQueryResponseItem
                {
                    PlannedValue = "11.00%",
                    FactFixedValue = "1 303.50",
                    ExpenseItem = new Queries.FinanceDistributionPlans.Details.ExpenseItem
                    {
                        Id = expenseItems[1].Id,
                        Name = expenseItems[1].Name,
                        Tags = []
                    }
                },
                new DetailsFinanceDistributionPlanQueryResponseItem
                {
                    PlannedValue = "17.00%",
                    FactFixedValue = "2 014.50",
                    ExpenseItem = new Queries.FinanceDistributionPlans.Details.ExpenseItem
                    {
                        Id = expenseItems[2].Id,
                        Name = expenseItems[2].Name,
                        Tags = []
                    }
                },
                new DetailsFinanceDistributionPlanQueryResponseItem
                {
                    PlannedValue = "72.00%",
                    FactFixedValue = "8 532.00",
                    ExpenseItem = new Queries.FinanceDistributionPlans.Details.ExpenseItem
                    {
                        Id = expenseItems[3].Id,
                        Name = expenseItems[3].Name,
                        Tags = []
                    }
                },
                new DetailsFinanceDistributionPlanQueryResponseItem
                {
                    PlannedValue = "7 000.00",
                    FactFixedValue = "7 000.00",
                    ExpenseItem = new Queries.FinanceDistributionPlans.Details.ExpenseItem
                    {
                        Id = expenseItemsWithTag[0].Id,
                        Name = expenseItemsWithTag[0].Name,
                        Tags = [expenseItemTag.Name]
                    }
                },
                new DetailsFinanceDistributionPlanQueryResponseItem
                {
                    PlannedValue = "1 000.00",
                    FactFixedValue = "650.00",
                    ExpenseItem = new Queries.FinanceDistributionPlans.Details.ExpenseItem
                    {
                        Id = expenseItemsWithTag[1].Id,
                        Name = expenseItemsWithTag[1].Name,
                        Tags = [expenseItemTag.Name]
                    }
                }
            ]
        };

        expectedResponse.TagsSum = expectedResponse.TagsSum.OrderBy(x => x.Key).ToDictionary();
        expectedResponse.Items = expectedResponse.Items.OrderBy(x => x.ExpenseItem.Name).ToList();

        var request = new DetailsFinanceDistributionPlanQueryRequest
        {
            FinanceDistributionPlanId = plan.Id,
            OwnerId = owner.Id
        };

        // Act
        var actualResponse = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.Equal(expectedResponse.AsJsonString(), actualResponse.AsJsonString());
    }
}