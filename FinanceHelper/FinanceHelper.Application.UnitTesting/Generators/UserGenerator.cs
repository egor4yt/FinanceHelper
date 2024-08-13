using FinanceHelper.Domain.Entities;
using FinanceHelper.Persistence;
using FinanceHelper.Shared;

namespace FinanceHelper.Application.UnitTesting.Generators;

public static class UserGenerator
{
    /// <summary>
    /// Generates user with some preferred localization code
    /// </summary>
    /// <param name="applicationDbContext"></param>
    /// <param name="preferredLocalization"></param>
    /// <returns></returns>
    public static async Task<User> SeedOneUserAsync(this ApplicationDbContext applicationDbContext, SupportedLanguage? preferredLocalization = null)
    {
        var entity = new User
        {
            Email = Guid.NewGuid().ToString(),
            PasswordHash = SecurityHelper.ComputeSha256Hash(Guid.NewGuid().ToString()),
            PreferredLocalization = preferredLocalization ?? await applicationDbContext.SeedOneSupportedLanguageAsync()
        };

        await applicationDbContext.AddAsync(entity);
        await applicationDbContext.SaveChangesAsync();

        return entity;
    }
}