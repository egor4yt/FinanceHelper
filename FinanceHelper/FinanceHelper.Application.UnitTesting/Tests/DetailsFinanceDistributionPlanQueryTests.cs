using FinanceHelper.Application.Queries.FinanceDistributionPlans.Details;
using FinanceHelper.Application.UnitTesting.Common;
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
        var owner = await UserGenerator.SeedOneAsync();
        var incomeSource = await IncomeSourceGenerator.SeedOneAsync();
        var expenseItem1 = await ExpenseItemGenerator.SeedOneAsync();
        var expenseItem2 = await ExpenseItemGenerator.SeedOneAsync();
        var expenseItem3 = await ExpenseItemGenerator.SeedOneAsync();
        var expenseItem4 = await ExpenseItemGenerator.SeedOneAsync();
        var expenseItem5 = await ExpenseItemGenerator.SeedOneAsync();
        var expenseItem6 = await ExpenseItemGenerator.SeedOneAsync();
        var expenseItem7 = await ExpenseItemGenerator.SeedOneAsync();
        var expenseItem8 = await ExpenseItemGenerator.SeedOneAsync();

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
                    StepNumber = 1,
                    PlannedValue = 10_000, // FACT = 10 000 * 0.65 = 6 500
                    ExpenseItemId = expenseItem1.Id,
                    ValueTypeCode = Domain.Metadata.FinancesDistributionItemValueType.Fixed.Code
                },
                new FinanceDistributionPlanItem
                {
                    StepNumber = 1,
                    PlannedValue = 11, // FACT = (26 000 - 6 500) * 0.11 = 2 145
                    ExpenseItemId = expenseItem2.Id,
                    ValueTypeCode = Domain.Metadata.FinancesDistributionItemValueType.Floating.Code
                },
                new FinanceDistributionPlanItem
                {
                    StepNumber = 1,
                    PlannedValue = 17, // FACT = (26 000 - 6 500) * 0.17 = 3 315
                    ExpenseItemId = expenseItem3.Id,
                    ValueTypeCode = Domain.Metadata.FinancesDistributionItemValueType.Floating.Code
                },

                // Available budget for next step = 26 000 - 6500 - 5460 = 14040
                new FinanceDistributionPlanItem
                {
                    StepNumber = 2,
                    PlannedValue = 27, // FACT = (14 040 - 325) * 0.27 = 3 703.05
                    ExpenseItemId = expenseItem4.Id,
                    ValueTypeCode = Domain.Metadata.FinancesDistributionItemValueType.Floating.Code
                },
                new FinanceDistributionPlanItem
                {
                    StepNumber = 2,
                    PlannedValue = 500, // FACT = 500 * 0.65 = 325
                    ExpenseItemId = expenseItem5.Id,
                    ValueTypeCode = Domain.Metadata.FinancesDistributionItemValueType.Fixed.Code
                },

                // Available budget for next step = 14040 - 325 - 3703.05 = 10 011.95
                new FinanceDistributionPlanItem
                {
                    StepNumber = 3,
                    PlannedValue = 5000, // FACT = 5000 * 0.65 = 3250
                    ExpenseItemId = expenseItem6.Id,
                    ValueTypeCode = Domain.Metadata.FinancesDistributionItemValueType.Fixed.Code
                },
                new FinanceDistributionPlanItem
                {
                    StepNumber = 3,
                    PlannedValue = 1500, // FACT = 1500 * 0.65 = 975
                    ExpenseItemId = expenseItem7.Id,
                    ValueTypeCode = Domain.Metadata.FinancesDistributionItemValueType.Fixed.Code
                },
                new FinanceDistributionPlanItem
                {
                    StepNumber = 3,
                    PlannedValue = 100, // FACT = (10 011.95 - 4225) * 1 = 5 786.95
                    ExpenseItemId = expenseItem8.Id,
                    ValueTypeCode = Domain.Metadata.FinancesDistributionItemValueType.Floating.Code
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
            IncomeSource = new Queries.FinanceDistributionPlans.Details.IncomeSource
            {
                Id = incomeSource.Id,
                Name = incomeSource.Name
            },
            Steps =
            [
                new StepGroup
                {
                    StepNumber = 1,
                    StepFixedExpenses = 6500.00M,
                    StepFloatedExpenses = 5460.00M,
                    Items =
                    [
                        new StepItem
                        {
                            PlannedValue = 10_000M,
                            PlannedValuePostfix = null!,
                            FactFixedValue = 6500.00M,
                            ExpenseItem = new Queries.FinanceDistributionPlans.Details.ExpenseItem
                            {
                                Id = expenseItem1.Id,
                                Name = expenseItem1.Name
                            }
                        },
                        new StepItem
                        {
                            PlannedValue = 11M,
                            PlannedValuePostfix = "%",
                            FactFixedValue = 2145.00M,
                            ExpenseItem = new Queries.FinanceDistributionPlans.Details.ExpenseItem
                            {
                                Id = expenseItem2.Id,
                                Name = expenseItem2.Name
                            }
                        },
                        new StepItem
                        {
                            PlannedValue = 17M,
                            PlannedValuePostfix = "%",
                            FactFixedValue = 3315.00M,
                            ExpenseItem = new Queries.FinanceDistributionPlans.Details.ExpenseItem
                            {
                                Id = expenseItem3.Id,
                                Name = expenseItem3.Name
                            }
                        }
                    ]
                },
                new StepGroup
                {
                    StepNumber = 2,
                    StepFixedExpenses = 325.00M,
                    StepFloatedExpenses = 3703.05M,
                    Items =
                    [
                        new StepItem
                        {
                            PlannedValue = 27M,
                            PlannedValuePostfix = "%",
                            FactFixedValue = 3703.05M,
                            ExpenseItem = new Queries.FinanceDistributionPlans.Details.ExpenseItem
                            {
                                Id = expenseItem4.Id,
                                Name = expenseItem4.Name
                            }
                        },
                        new StepItem
                        {
                            PlannedValue = 500M,
                            PlannedValuePostfix = null!,
                            FactFixedValue = 325.00M,
                            ExpenseItem = new Queries.FinanceDistributionPlans.Details.ExpenseItem
                            {
                                Id = expenseItem5.Id,
                                Name = expenseItem5.Name
                            }
                        }
                    ]
                },
                new StepGroup
                {
                    StepNumber = 3,
                    StepFixedExpenses = 4225.00M,
                    StepFloatedExpenses = 5786.95M,
                    Items =
                    [
                        new StepItem
                        {
                            PlannedValue = 5000M,
                            PlannedValuePostfix = null!,
                            FactFixedValue = 3250.00M,
                            ExpenseItem = new Queries.FinanceDistributionPlans.Details.ExpenseItem
                            {
                                Id = expenseItem6.Id,
                                Name = expenseItem6.Name
                            }
                        },
                        new StepItem
                        {
                            PlannedValue = 1500M,
                            PlannedValuePostfix = null!,
                            FactFixedValue = 975.00M,
                            ExpenseItem = new Queries.FinanceDistributionPlans.Details.ExpenseItem
                            {
                                Id = expenseItem7.Id,
                                Name = expenseItem7.Name
                            }
                        },
                        new StepItem
                        {
                            PlannedValue = 100M,
                            PlannedValuePostfix = "%",
                            FactFixedValue = 5786.95M,
                            ExpenseItem = new Queries.FinanceDistributionPlans.Details.ExpenseItem
                            {
                                Id = expenseItem8.Id,
                                Name = expenseItem8.Name
                            }
                        }
                    ]
                }
            ]
        };

        expectedResponse.Steps = expectedResponse.Steps.OrderBy(x => x.StepNumber).ToList();
        expectedResponse.Steps.ForEach(x => x.Items = x.Items.OrderBy(y => y.ExpenseItem.Name).ToList());

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