using FinanceHelper.Domain.Entities;
using FinanceHelper.Persistence;

namespace FinanceHelper.Application.UnitTesting.Generators;

public static class SupportedLanguageGenerator
{
    public static async Task<SupportedLanguage> SeedOneSupportedLanguageAsync(this ApplicationDbContext applicationDbContext)
    {
        var entity = new SupportedLanguage
        {
            Code = Guid.NewGuid().ToString(),
            LocalizedValue = Guid.NewGuid().ToString()
        };

        await applicationDbContext.AddAsync(entity);
        await applicationDbContext.SaveChangesAsync();

        return entity;
    }
}