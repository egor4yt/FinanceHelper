﻿using FinanceHelper.Domain.Entities;
using FinanceHelper.Persistence;

namespace FinanceHelper.Application.UnitTesting.Generators;

public static class ExpenseItemGenerator
{
    /// <summary>
    ///     Generates expense item with some type for some user
    /// </summary>
    public static async Task<ExpenseItem> SeedOneExpenseItemAsync(this ApplicationDbContext applicationDbContext, User? user = null, ExpenseItemType? expenseItemType = null, List<Tag>? tags = null)
    {
        var entity = new ExpenseItem
        {
            Name = Guid.NewGuid().ToString(),
            ExpenseItemType = expenseItemType ?? await applicationDbContext.SeedOneExpenseItemTypeAsync(),
            Color = Guid.NewGuid().ToString(),
            Owner = user ?? await applicationDbContext.SeedOneUserAsync()
        };

        await applicationDbContext.AddAsync(entity);

        var tagMap = tags?.Select(tag => new TagMap
        {
            TagId = tag.Id,
            EntityId = entity.Id
        }).ToList();
        if (tagMap?.Count > 0) await applicationDbContext.AddRangeAsync(tagMap!);

        await applicationDbContext.SaveChangesAsync();

        return entity;
    }

    /// <summary>
    ///     Generates many expense items with some type for some user
    /// </summary>
    public static async Task<List<ExpenseItem>> SeedManyExpenseItemAsync(this ApplicationDbContext applicationDbContext, int count, User? user = null, ExpenseItemType? expenseItemType = null, IEnumerable<Tag>? tags = null)
    {
        var owner = user ?? await applicationDbContext.SeedOneUserAsync();
        var targetExpenseItemType = expenseItemType ?? await applicationDbContext.SeedOneExpenseItemTypeAsync();

        var entities = Enumerable.Range(0, count).Select(_ => new ExpenseItem
            {
                Name = Guid.NewGuid().ToString(),
                ExpenseItemType = targetExpenseItemType,
                Color = Guid.NewGuid().ToString(),
                Owner = owner
            })
            .ToList();

        await applicationDbContext.AddRangeAsync(entities);

        var tagMap = tags?.SelectMany(tag => entities.Select(entity => new TagMap
        {
            TagId = tag.Id,
            EntityId = entity.Id
        })).ToList();

        if (tagMap?.Count > 0) await applicationDbContext.AddRangeAsync(tagMap!);

        await applicationDbContext.SaveChangesAsync();

        return entities;
    }
}