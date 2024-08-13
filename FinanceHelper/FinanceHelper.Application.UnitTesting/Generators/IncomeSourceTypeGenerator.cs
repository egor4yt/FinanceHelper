using FinanceHelper.Domain.Entities;
using FinanceHelper.Persistence;

namespace FinanceHelper.Application.UnitTesting.Generators;

public static class IncomeSourceTypeGenerator
{
    public static async Task<IncomeSourceType> SeedOneIncomeSourceTypeAsync(this ApplicationDbContext applicationDbContext)
    {
        var entity = new IncomeSourceType
        {
            Code = Guid.NewGuid().ToString(),
            LocalizationKeyword = Guid.NewGuid().ToString()
        };

        var localizationEntity = new MetadataLocalization
        {
            LocalizedValue = Guid.NewGuid().ToString(),
            LocalizationKeyword = entity.LocalizationKeyword,
            MetadataTypeCode = Domain.Metadata.MetadataType.IncomeSourceType.Code,
            SupportedLanguage = await applicationDbContext.SeedOneSupportedLanguageAsync()
        };

        await applicationDbContext.AddRangeAsync(entity, localizationEntity);
        await applicationDbContext.SaveChangesAsync();

        return entity;
    }
}