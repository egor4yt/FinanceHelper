﻿using FinanceHelper.Domain.Entities;
using FinanceHelper.Persistence;
using FinanceHelper.Shared;

namespace FinanceHelper.Application.UnitTesting.Generators;

public class UserGenerator(ApplicationDbContext applicationDbContext)
{
    public async Task<User> SeedOneAsync()
    {
        var entity = GenerateEntity();

        await applicationDbContext.AddAsync(entity);
        await applicationDbContext.SaveChangesAsync();

        return entity;
    }

    public static User GenerateEntity()
    {
        return new User
        {
            Email = Guid.NewGuid().ToString(),
            PasswordHash = SecurityHelper.ComputeSha256Hash(Guid.NewGuid().ToString()),
            PreferredLocalizationCode = Guid.NewGuid().ToString()
        };
    }
}