using FinanceHelper.Domain.Entities;
using FinanceHelper.Persistence;
using FinanceHelper.Shared;

namespace FinanceHelper.Application.UnitTesting.Generators;

public class UserGenerator(ApplicationDbContext applicationDbContext)
{
    public async Task<User> SeedOneRandomUserAsync()
    {
        var user = new User
        {
            Id = new Random().Next(),
            Email = Guid.NewGuid().ToString(),
            PasswordHash = SecurityHelper.ComputeSha256Hash(Guid.NewGuid().ToString()),
            PreferredLocalizationCode = new Guid().ToString()
        };
        await applicationDbContext.AddAsync(user);
        await applicationDbContext.SaveChangesAsync();
        return user;
    }
}