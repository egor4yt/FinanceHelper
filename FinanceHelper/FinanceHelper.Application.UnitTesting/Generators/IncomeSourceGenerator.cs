using FinanceHelper.Domain.Entities;
using FinanceHelper.Persistence;

namespace FinanceHelper.Application.UnitTesting.Generators;

public static class IncomeSourceGenerator
{
    public static async Task<IncomeSource> SeedOneIncomeSourceAsync(this ApplicationDbContext applicationDbContext, User? user = null, IncomeSourceType? incomeSourceType = null)
    {
        var entity = new IncomeSource
        {
            IncomeSourceType = incomeSourceType ?? await applicationDbContext.SeedOneIncomeSourceTypeAsync(),
            Name = Guid.NewGuid().ToString(),
            Color = Guid.NewGuid().ToString(),
            Owner = user ?? await applicationDbContext.SeedOneUserAsync()
        };

        await applicationDbContext.AddAsync(entity);
        await applicationDbContext.SaveChangesAsync();

        return entity;
    }
}