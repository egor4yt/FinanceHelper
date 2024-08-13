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


    /// <summary>
    ///     Generates many income sources with some type for some user
    /// </summary>
    public static async Task<List<IncomeSource>> SeedManyIncomeSourceAsync(this ApplicationDbContext applicationDbContext, int count, User? user = null, IncomeSourceType? incomeSourceType = null)
    {
        var owner = user ?? await applicationDbContext.SeedOneUserAsync();
        var targetIncomeSourceType = incomeSourceType ?? await applicationDbContext.SeedOneIncomeSourceTypeAsync();

        var entities = Enumerable.Range(0, count).Select(x => new IncomeSource
            {
                Name = Guid.NewGuid().ToString(),
                IncomeSourceType = targetIncomeSourceType,
                Color = Guid.NewGuid().ToString(),
                Owner = owner
            })
            .ToList();

        await applicationDbContext.AddRangeAsync(entities);
        await applicationDbContext.SaveChangesAsync();

        return entities;
    }
}