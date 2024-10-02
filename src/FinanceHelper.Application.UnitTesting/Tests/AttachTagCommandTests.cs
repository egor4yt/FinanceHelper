using FinanceHelper.Application.Commands.Tags.Attach;
using FinanceHelper.Application.Exceptions;
using FinanceHelper.Application.UnitTesting.Common;
using FinanceHelper.Application.UnitTesting.Generators;
using Microsoft.EntityFrameworkCore;

namespace FinanceHelper.Application.UnitTesting.Tests;

public class AttachTagCommandTests : TestBase<AttachTagCommandHandler>
{
    private readonly AttachTagCommandHandler _handler;

    public AttachTagCommandTests()
    {
        _handler = new AttachTagCommandHandler(ApplicationDbContext, Localizer);
    }

    [Fact]
    public async Task Success_ExpenseItem()
    {
        // Arrange
        var tagType = Domain.Metadata.TagType.ExpenseItem.Code;
        var owner = await ApplicationDbContext.SeedOneUserAsync();
        var tag = await ApplicationDbContext.SeedOneTagAsync(tagType, owner);
        var expenseItem = await ApplicationDbContext.SeedOneExpenseItemAsync(owner);
        var request = new AttachTagCommandRequest
        {
            EntityId = expenseItem.Id,
            TagId = tag.Id,
            OwnerId = owner.Id,
            TagTypeCode = tagType
        };

        // Act
        await _handler.Handle(request, CancellationToken.None);

        var databaseObject = await ApplicationDbContext.TagsMap
            .SingleOrDefaultAsync(x => x.EntityId == expenseItem.Id
                                       && x.TagId == tag.Id);

        // Assert
        Assert.NotNull(databaseObject);
    }

    [Fact]
    public async Task Success_IncomeSource()
    {
        // Arrange
        var tagType = Domain.Metadata.TagType.IncomeSource.Code;
        var owner = await ApplicationDbContext.SeedOneUserAsync();
        var tag = await ApplicationDbContext.SeedOneTagAsync(tagType, owner);
        var incomeSource = await ApplicationDbContext.SeedOneIncomeSourceAsync(owner);

        var request = new AttachTagCommandRequest
        {
            EntityId = incomeSource.Id,
            TagId = tag.Id,
            OwnerId = owner.Id,
            TagTypeCode = tagType
        };

        // Act
        await _handler.Handle(request, CancellationToken.None);

        var databaseObject = await ApplicationDbContext.TagsMap
            .SingleOrDefaultAsync(x => x.EntityId == incomeSource.Id
                                       && x.TagId == tag.Id);

        // Assert
        Assert.NotNull(databaseObject);
    }

    [Fact]
    public async Task NotSupportedTagTypeCode()
    {
        var tagType = Guid.NewGuid().ToString();
        var tag = await ApplicationDbContext.SeedOneTagAsync(tagType);
        var owner = await ApplicationDbContext.SeedOneUserAsync();
        var expenseItem = await ApplicationDbContext.SeedOneExpenseItemAsync();

        // Arrange
        var request = new AttachTagCommandRequest
        {
            EntityId = expenseItem.Id,
            TagId = tag.Id,
            OwnerId = owner.Id,
            TagTypeCode = tagType
        };

        // Assert
        await Assert.ThrowsAsync<BadRequestException>(async () => await _handler.Handle(request, CancellationToken.None));
    }

    [Fact]
    public async Task DoesNotExistsTagId()
    {
        var tagType = Guid.NewGuid().ToString();
        var tag = await ApplicationDbContext.SeedOneTagAsync(tagType);
        var owner = await ApplicationDbContext.SeedOneUserAsync();

        // Arrange
        var request = new AttachTagCommandRequest
        {
            EntityId = -1,
            TagId = tag.Id,
            OwnerId = owner.Id,
            TagTypeCode = tagType
        };

        // Assert
        await Assert.ThrowsAsync<BadRequestException>(async () => await _handler.Handle(request, CancellationToken.None));
    }

    [Fact]
    public async Task DoesNotExistsEntityId()
    {
        var tagType = Domain.Metadata.TagType.ExpenseItem.Code;
        var owner = await ApplicationDbContext.SeedOneUserAsync();
        var expenseItem = await ApplicationDbContext.SeedOneExpenseItemAsync();

        // Arrange
        var request = new AttachTagCommandRequest
        {
            EntityId = expenseItem.Id,
            TagId = -1,
            OwnerId = owner.Id,
            TagTypeCode = tagType
        };

        // Assert
        await Assert.ThrowsAsync<BadRequestException>(async () => await _handler.Handle(request, CancellationToken.None));
    }

    [Fact]
    public async Task OtherExpenseItem()
    {
        var tagType = Domain.Metadata.TagType.IncomeSource.Code;
        var owner = await ApplicationDbContext.SeedOneUserAsync();
        var tag = await ApplicationDbContext.SeedOneTagAsync(tagType, owner);
        var notMyExpenseItem = await ApplicationDbContext.SeedOneExpenseItemAsync();

        // Arrange
        var request = new AttachTagCommandRequest
        {
            EntityId = notMyExpenseItem.Id,
            TagId = tag.Id,
            OwnerId = owner.Id,
            TagTypeCode = tagType
        };

        // Assert
        await Assert.ThrowsAsync<BadRequestException>(async () => await _handler.Handle(request, CancellationToken.None));
    }

    [Fact]
    public async Task OtherTag()
    {
        var tagType = Domain.Metadata.TagType.IncomeSource.Code;
        var owner = await ApplicationDbContext.SeedOneUserAsync();
        var notMyTag = await ApplicationDbContext.SeedOneTagAsync(tagType);
        var expenseItem = await ApplicationDbContext.SeedOneExpenseItemAsync(owner);

        // Arrange
        var request = new AttachTagCommandRequest
        {
            EntityId = expenseItem.Id,
            TagId = notMyTag.Id,
            OwnerId = owner.Id,
            TagTypeCode = tagType
        };

        // Assert
        await Assert.ThrowsAsync<BadRequestException>(async () => await _handler.Handle(request, CancellationToken.None));
    }
}