using FinanceHelper.Domain.Entities;
using FinanceHelper.Persistence;

namespace FinanceHelper.Application.UnitTesting.Generators;

public static class TagGenerator
{
    public static async Task<Tag> SeedOneTagAsync(this ApplicationDbContext applicationDbContext, string tagTypeCode, User? user = null)
    {
        var entity = new Tag
        {
            Name = Guid.NewGuid().ToString(),
            Owner = user ?? await applicationDbContext.SeedOneUserAsync(),
            TagTypeCode = tagTypeCode
        };

        await applicationDbContext.AddAsync(entity);
        await applicationDbContext.SaveChangesAsync();

        return entity;
    }

    public static async Task<List<Tag>> SeedManyTagsAsync(this ApplicationDbContext applicationDbContext, int count, string tagTypeCode, User? user = null)
    {
        var owner = user ?? await applicationDbContext.SeedOneUserAsync();

        var entities = Enumerable.Range(0, count).Select(_ => new Tag
            {
                Name = Guid.NewGuid().ToString(),
                TagTypeCode = tagTypeCode,
                Owner = owner
            })
            .ToList();

        await applicationDbContext.AddRangeAsync(entities);
        await applicationDbContext.SaveChangesAsync();

        return entities;
    }
}