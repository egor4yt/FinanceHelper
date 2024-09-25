// using FinanceHelper.Application.Queries.FinanceDistributionPlans.Details;
// using FinanceHelper.Application.UnitTesting.Common;
// using FinanceHelper.Application.UnitTesting.Generators;
// using FinanceHelper.Domain.Entities;
//
// namespace FinanceHelper.Application.UnitTesting.Tests;
//
// public class DetailsFinanceDistributionPlanQueryTests : TestBase<DetailsFinanceDistributionPlanQueryHandler>
// {
//     private readonly DetailsFinanceDistributionPlanQueryHandler _handler;
//
//     public DetailsFinanceDistributionPlanQueryTests()
//     {
//         _handler = new DetailsFinanceDistributionPlanQueryHandler(ApplicationDbContext, Localizer);
//     }
//
//     [Fact]
//     public async Task Success()
//     {
//         // Arrange
//         var owner = await ApplicationDbContext.SeedOneUserAsync();
//         var incomeSource = await ApplicationDbContext.SeedOneIncomeSourceAsync(owner);
//         var expenseItems = await ApplicationDbContext.SeedManyExpenseItemAsync(7, owner);
//         var expenseItemTags = await ApplicationDbContext.SeedManyTagsAsync(1, Domain.Metadata.TagType.ExpenseItem.Code, owner);
//         var expenseItemWithTag = await ApplicationDbContext.SeedOneExpenseItemAsync(owner, null, expenseItemTags);
//
//         var plan = new FinanceDistributionPlan
//         {
//             Id = 0,
//             PlannedBudget = 40_000,
//             FactBudget = 26_000, // Factor = 26 000 / 40 000 = 0.65
//             CreatedAt = DateTime.UtcNow,
//             OwnerId = owner.Id,
//             IncomeSourceId = incomeSource.Id,
//             FinanceDistributionPlanItems = new List<FinanceDistributionPlanItem>
//             {
//                 new FinanceDistributionPlanItem
//                 {
//                     StepNumber = 1,
//                     PlannedValue = 10_000, // FACT = 10 000 * 0.65 = 6 500
//                     ExpenseItemId = expenseItems[0].Id,
//                     ValueTypeCode = Domain.Metadata.FinancesDistributionItemValueType.Fixed.Code
//                 },
//                 new FinanceDistributionPlanItem
//                 {
//                     StepNumber = 1,
//                     PlannedValue = 11, // FACT = (26 000 - 6 500) * 0.11 = 2 145
//                     ExpenseItemId = expenseItems[1].Id,
//                     ValueTypeCode = Domain.Metadata.FinancesDistributionItemValueType.Floating.Code
//                 },
//                 new FinanceDistributionPlanItem
//                 {
//                     StepNumber = 1,
//                     PlannedValue = 17, // FACT = (26 000 - 6 500) * 0.17 = 3 315
//                     ExpenseItemId = expenseItems[2].Id,
//                     ValueTypeCode = Domain.Metadata.FinancesDistributionItemValueType.Floating.Code
//                 },
//
//                 // Available budget for next step = 26 000 - 6500 - 5460 = 14 040
//                 new FinanceDistributionPlanItem
//                 {
//                     StepNumber = 2,
//                     PlannedValue = 27, // FACT = (14 040 - 500) * 0.27 = 3 655.8
//                     ExpenseItemId = expenseItems[3].Id,
//                     ValueTypeCode = Domain.Metadata.FinancesDistributionItemValueType.Floating.Code
//                 },
//                 new FinanceDistributionPlanItem
//                 {
//                     StepNumber = 2,
//                     PlannedValue = 500, // FACT = 500
//                     ExpenseItemId = expenseItems[4].Id,
//                     ValueTypeCode = Domain.Metadata.FinancesDistributionItemValueType.FixedIndivisible.Code
//                 },
//
//                 // Available budget for next step = 14 040 - 500 - 3 655.8 = 9 884.2
//                 new FinanceDistributionPlanItem
//                 {
//                     StepNumber = 3,
//                     PlannedValue = 5000, // FACT = 5000 * 0.65 = 3250
//                     ExpenseItemId = expenseItems[5].Id,
//                     ValueTypeCode = Domain.Metadata.FinancesDistributionItemValueType.Fixed.Code
//                 },
//                 new FinanceDistributionPlanItem
//                 {
//                     StepNumber = 3,
//                     PlannedValue = 1500, // FACT = 1500 * 0.65 = 975
//                     ExpenseItemId = expenseItems[6].Id,
//                     ValueTypeCode = Domain.Metadata.FinancesDistributionItemValueType.Fixed.Code
//                 },
//                 new FinanceDistributionPlanItem
//                 {
//                     StepNumber = 3,
//                     PlannedValue = 100, // FACT = (9 884.2 - 4225) * 1 = 5 659.2
//                     ExpenseItemId = expenseItemWithTag.Id,
//                     ValueTypeCode = Domain.Metadata.FinancesDistributionItemValueType.Floating.Code
//                 }
//             }
//         };
//
//         await ApplicationDbContext.AddAsync(plan);
//         await ApplicationDbContext.SaveChangesAsync();
//
//         var expectedResponse = new DetailsFinanceDistributionPlanQueryResponse
//         {
//             PlannedBudget = plan.PlannedBudget,
//             FactBudget = plan.FactBudget,
//             CreatedAt = plan.CreatedAt,
//             IncomeSource = new Queries.FinanceDistributionPlans.Details.IncomeSource
//             {
//                 Id = incomeSource.Id,
//                 Name = incomeSource.Name
//             },
//             Steps =
//             [
//                 new StepGroup
//                 {
//                     StepNumber = 1,
//                     Items =
//                     [
//                         new StepItem
//                         {
//                             PlannedValue = 10_000M,
//                             PlannedValuePostfix = null!,
//                             FactFixedValue = 6500.00M,
//                             ExpenseItem = new Queries.FinanceDistributionPlans.Details.ExpenseItem
//                             {
//                                 Id = expenseItems[0].Id,
//                                 Name = expenseItems[0].Name,
//                                 Tags = []
//                             }
//                         },
//                         new StepItem
//                         {
//                             PlannedValue = 11M,
//                             PlannedValuePostfix = "%",
//                             FactFixedValue = 2145.00M,
//                             ExpenseItem = new Queries.FinanceDistributionPlans.Details.ExpenseItem
//                             {
//                                 Id = expenseItems[1].Id,
//                                 Name = expenseItems[1].Name,
//                                 Tags = []
//                             }
//                         },
//                         new StepItem
//                         {
//                             PlannedValue = 17M,
//                             PlannedValuePostfix = "%",
//                             FactFixedValue = 3315.00M,
//                             ExpenseItem = new Queries.FinanceDistributionPlans.Details.ExpenseItem
//                             {
//                                 Id = expenseItems[2].Id,
//                                 Name = expenseItems[2].Name,
//                                 Tags = []
//                             }
//                         }
//                     ]
//                 },
//                 new StepGroup
//                 {
//                     StepNumber = 2,
//                     Items =
//                     [
//                         new StepItem
//                         {
//                             PlannedValue = 27M,
//                             PlannedValuePostfix = "%",
//                             FactFixedValue = 3655.80M,
//                             ExpenseItem = new Queries.FinanceDistributionPlans.Details.ExpenseItem
//                             {
//                                 Id = expenseItems[3].Id,
//                                 Name = expenseItems[3].Name,
//                                 Tags = []
//                             }
//                         },
//                         new StepItem
//                         {
//                             PlannedValue = 500M,
//                             PlannedValuePostfix = null!,
//                             FactFixedValue = 500M,
//                             ExpenseItem = new Queries.FinanceDistributionPlans.Details.ExpenseItem
//                             {
//                                 Id = expenseItems[4].Id,
//                                 Name = expenseItems[4].Name,
//                                 Tags = []
//                             }
//                         }
//                     ]
//                 },
//                 new StepGroup
//                 {
//                     StepNumber = 3,
//                     Items =
//                     [
//                         new StepItem
//                         {
//                             PlannedValue = 5000M,
//                             PlannedValuePostfix = null!,
//                             FactFixedValue = 3250.00M,
//                             ExpenseItem = new Queries.FinanceDistributionPlans.Details.ExpenseItem
//                             {
//                                 Id = expenseItems[5].Id,
//                                 Name = expenseItems[5].Name,
//                                 Tags = []
//                             }
//                         },
//                         new StepItem
//                         {
//                             PlannedValue = 1500M,
//                             PlannedValuePostfix = null!,
//                             FactFixedValue = 975.00M,
//                             ExpenseItem = new Queries.FinanceDistributionPlans.Details.ExpenseItem
//                             {
//                                 Id = expenseItems[6].Id,
//                                 Name = expenseItems[6].Name,
//                                 Tags = []
//                             }
//                         },
//                         new StepItem
//                         {
//                             PlannedValue = 100M,
//                             PlannedValuePostfix = "%",
//                             FactFixedValue = 5659.20M,
//                             ExpenseItem = new Queries.FinanceDistributionPlans.Details.ExpenseItem
//                             {
//                                 Id = expenseItemWithTag.Id,
//                                 Name = expenseItemWithTag.Name,
//                                 Tags = expenseItemTags.Select(x => x.Name).Order().ToList()
//                             }
//                         }
//                     ]
//                 }
//             ]
//         };
//
//         expectedResponse.Steps = expectedResponse.Steps.OrderBy(x => x.StepNumber).ToList();
//         expectedResponse.Steps.ForEach(x => x.Items = x.Items.OrderBy(y => y.ExpenseItem.Name).ToList());
//
//         var request = new DetailsFinanceDistributionPlanQueryRequest
//         {
//             FinanceDistributionPlanId = plan.Id,
//             OwnerId = owner.Id
//         };
//
//         // Act
//         var actualResponse = await _handler.Handle(request, CancellationToken.None);
//
//         // Assert
//         Assert.Equal(expectedResponse.AsJsonString(), actualResponse.AsJsonString());
//     }
// }