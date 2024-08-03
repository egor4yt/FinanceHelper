using FinanceHelper.Domain.Entities;
using FinanceHelper.Persistence;

namespace FinanceHelper.Application.UnitTesting.Generators;

public class ExpenseItemGenerator(ApplicationDbContext applicationDbContext)
{
    public async Task<ExpenseItem> SeedOneAsync(User? user = null)
    {
        var entity = new ExpenseItem
        {
            Name = Guid.NewGuid().ToString(),
            ExpenseItemTypeCode = Guid.NewGuid().ToString(),
            Color = Guid.NewGuid().ToString(),
            OwnerId = user?.Id ?? new Random().NextInt64()
        };

        await applicationDbContext.AddAsync(entity);
        await applicationDbContext.SaveChangesAsync();

        return entity;
    }
}