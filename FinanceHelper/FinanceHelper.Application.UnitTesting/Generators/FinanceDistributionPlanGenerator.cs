using FinanceHelper.Domain.Entities;
using FinanceHelper.Persistence;

namespace FinanceHelper.Application.UnitTesting.Generators;

public static class FinanceDistributionPlanGenerator
{
    public static async Task<FinanceDistributionPlan> SeedOneFinanceDistributionPlanAsync(this ApplicationDbContext applicationDbContext, User? user = null, IncomeSource? incomeSource = null)
    {
        var random1 = new Random().Next() + (decimal)new Random().NextDouble();
        var random2 = new Random().Next() + (decimal)new Random().NextDouble();
        var owner = user ?? await applicationDbContext.SeedOneUserAsync();
        var targetIncomeSource = incomeSource ?? await applicationDbContext.SeedOneIncomeSourceAsync(owner);

        var entity = new FinanceDistributionPlan
        {
            PlannedBudget = Math.Max(random1, random2),
            FactBudget = Math.Min(random1, random2),
            CreatedAt = DateTime.UtcNow,
            Owner = owner,
            IncomeSource = targetIncomeSource
        };

        await applicationDbContext.AddAsync(entity);
        await applicationDbContext.SaveChangesAsync();

        return entity;
    }
}