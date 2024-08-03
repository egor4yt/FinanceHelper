using FinanceHelper.Domain.Entities;
using FinanceHelper.Persistence;

namespace FinanceHelper.Application.UnitTesting.Generators;

public class ExpenseItemTypeGenerator(ApplicationDbContext applicationDbContext)
{
    public async Task<ExpenseItemType> SeedOneAsync()
    {
        var entity = new ExpenseItemType
        {
            Code = Guid.NewGuid().ToString(),
            LocalizationKeyword = Guid.NewGuid().ToString()
        };

        await applicationDbContext.AddAsync(entity);
        await applicationDbContext.SaveChangesAsync();

        return entity;
    }
}