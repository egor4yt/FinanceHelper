using FinanceHelper.Domain.Entities;
using FinanceHelper.Persistence;

namespace FinanceHelper.Application.UnitTesting.Generators;

public static class ExpenseItemTypeGenerator
{
    public static async Task<ExpenseItemType> SeedOneExpenseItemTypeAsync(this ApplicationDbContext applicationDbContext)
    {
        var entity = new ExpenseItemType
        {
            Code = Guid.NewGuid().ToString(),
            LocalizationKeyword = Guid.NewGuid().ToString()
        };

        var localizationEntity = new MetadataLocalization
        {
            LocalizedValue = Guid.NewGuid().ToString(),
            LocalizationKeyword = entity.LocalizationKeyword,
            MetadataTypeCode = Domain.Metadata.MetadataType.ExpenseItemType.Code,
            SupportedLanguage = await applicationDbContext.SeedOneSupportedLanguageAsync()
        };

        await applicationDbContext.AddRangeAsync(entity, localizationEntity);
        await applicationDbContext.SaveChangesAsync();

        return entity;
    }
}