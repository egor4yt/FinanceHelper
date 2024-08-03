using FinanceHelper.Domain.Entities;
using FinanceHelper.Persistence;

namespace FinanceHelper.Application.UnitTesting.Generators;

public class IncomeSourceTypeGenerator(ApplicationDbContext applicationDbContext)
{
    public async Task<IncomeSourceType> SeedOneAsync()
    {
        var entity = new IncomeSourceType
        {
            Code = Guid.NewGuid().ToString(),
            LocalizationKeyword = Guid.NewGuid().ToString()
        };

        await applicationDbContext.AddAsync(entity);
        await applicationDbContext.SaveChangesAsync();

        return entity;
    }
}