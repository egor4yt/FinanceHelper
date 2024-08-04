using FinanceHelper.Domain.Entities;
using FinanceHelper.Persistence;

namespace FinanceHelper.Application.UnitTesting.Generators;

public class IncomeSourceGenerator(ApplicationDbContext applicationDbContext)
{
    public async Task<IncomeSource> SeedOneAsync(User? user = null, IncomeSourceType? incomeSourceType = null)
    {
        var entity = new IncomeSource
        {
            IncomeSourceTypeCode = incomeSourceType?.Code ?? Guid.NewGuid().ToString(),
            Name = Guid.NewGuid().ToString(),
            Color = Guid.NewGuid().ToString(),
            OwnerId = user?.Id ?? new Random().NextInt64(),
        };

        await applicationDbContext.AddAsync(entity);
        await applicationDbContext.SaveChangesAsync();

        return entity;
    }
}