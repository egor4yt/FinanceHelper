using FinanceHelper.Domain.Entities;
using FinanceHelper.Persistence;

namespace FinanceHelper.Application.UnitTesting.Generators;

public static class FinanceDistributionPlanItemGenerator
{
    public static async Task<FinanceDistributionPlanItem> SeedOneFinanceDistributionPlanItemAsync(this ApplicationDbContext applicationDbContext, FinanceDistributionPlan? plan = null, ExpenseItem? expenseItem = null)
    {
        var plannedValue = new Random().Next() + (decimal)new Random().NextDouble();
        var targetPlan = plan ?? await applicationDbContext.SeedOneFinanceDistributionPlanAsync();
        var targetExpenseItem = expenseItem ?? await applicationDbContext.SeedOneExpenseItemAsync();

        var entity = new FinanceDistributionPlanItem
        {
            PlannedValue = plannedValue,
            ValueTypeCode = Guid.NewGuid().ToString(),
            FinanceDistributionPlan = targetPlan,
            ExpenseItem = targetExpenseItem
        };

        await applicationDbContext.AddAsync(entity);
        await applicationDbContext.SaveChangesAsync();

        return entity;
    }
}