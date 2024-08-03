using FinanceHelper.Domain.Entities;
using FinanceHelper.Persistence;
using FinanceHelper.Shared;

namespace FinanceHelper.Application.UnitTesting.Generators;

public class UserGenerator(ApplicationDbContext applicationDbContext)
{
    public async Task<User> SeedOneAsync()
    {
        var entity = new User
        {
            Email = Guid.NewGuid().ToString(),
            PasswordHash = SecurityHelper.ComputeSha256Hash(Guid.NewGuid().ToString()),
            PreferredLocalizationCode = Guid.NewGuid().ToString()
        };

        await applicationDbContext.AddAsync(entity);
        await applicationDbContext.SaveChangesAsync();

        return entity;
    }
}