using FinanceHelper.Domain.Entities;
using FinanceHelper.Persistence;
using FinanceHelper.Shared;

namespace FinanceHelper.Application.UnitTesting.Generators;

public class SupportedLanguageGenerator(ApplicationDbContext applicationDbContext)
{
    public async Task<SupportedLanguage> SeedOneAsync()
    {
        var entity = GenerateEntity();

        await applicationDbContext.AddAsync(entity);
        await applicationDbContext.SaveChangesAsync();

        return entity;
    }

    public static SupportedLanguage GenerateEntity()
    {
        return new SupportedLanguage
        {
            Code = Guid.NewGuid().ToString(),
            LocalizedValue = Guid.NewGuid().ToString()
        };
    }
}