using FinanceHelper.Domain.Entities;
using FinanceHelper.Persistence;

namespace FinanceHelper.Application.UnitTesting.Generators;

public class FinanceDistributionPlanGenerator(ApplicationDbContext applicationDbContext)
{
    public async Task<FinanceDistributionPlan> SeedOneAsync(User? user, IncomeSource? incomeSource)
    {
        var random1 = new Random().Next() + (decimal)new Random().NextDouble();
        var random2 = new Random().Next() + (decimal)new Random().NextDouble();
        var entity = new FinanceDistributionPlan
        {
            PlannedBudget = Math.Max(random1, random2),
            FactBudget = Math.Min(random1, random2),
            CreatedAt = DateTime.UtcNow,
            OwnerId = user?.Id ?? new Random().NextInt64(),
            IncomeSourceId = incomeSource?.Id ?? new Random().NextInt64()
        };

        await applicationDbContext.AddAsync(entity);
        await applicationDbContext.SaveChangesAsync();

        return entity;
    }
}