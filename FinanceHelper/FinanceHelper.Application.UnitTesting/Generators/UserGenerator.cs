using FinanceHelper.Domain.Entities;
using FinanceHelper.Persistence;
using FinanceHelper.Shared;

namespace FinanceHelper.Application.UnitTesting.Generators;

public static class UserGenerator
{
    public static async Task<User> SeedOneUserAsync(this ApplicationDbContext applicationDbContext)
    {
        var entity = new User
        {
            Email = Guid.NewGuid().ToString(),
            PasswordHash = SecurityHelper.ComputeSha256Hash(Guid.NewGuid().ToString()),
            PreferredLocalizationCode = Guid.NewGuid().ToString() // TODO: must be reference to supported languages table
        };

        await applicationDbContext.AddAsync(entity);
        await applicationDbContext.SaveChangesAsync();

        return entity;
    }
}