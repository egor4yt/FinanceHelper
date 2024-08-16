using FinanceHelper.Application.Queries.ExpenseItems.GetUser;
using FinanceHelper.Application.UnitTesting.Common;
using FinanceHelper.Application.UnitTesting.Generators;
using Microsoft.EntityFrameworkCore;

namespace FinanceHelper.Application.UnitTesting.Tests;

public class GetUserExpenseItemQueryTests : TestBase<GetUserExpenseItemQueryHandler>
{
    private readonly GetUserExpenseItemQueryHandler _handler;

    public GetUserExpenseItemQueryTests()
    {
        _handler = new GetUserExpenseItemQueryHandler(ApplicationDbContext);
    }

    [Fact]
    public async Task Success()
    {
        // Arrange
        await ApplicationDbContext.SeedOneExpenseItemAsync(); // Generate random expense item

        var owner = await ApplicationDbContext.SeedOneUserAsync();
        var tags = await ApplicationDbContext.SeedManyTagsAsync(1, Domain.Metadata.TagType.ExpenseItem.Code, owner);
        var userExpenseItemType = await ApplicationDbContext.SeedOneExpenseItemTypeAsync(owner.PreferredLocalization);
        var userExpenseWithTags = await ApplicationDbContext.SeedOneExpenseItemAsync(owner, userExpenseItemType, tags);
        var userExpenseItemWithoutTags = await ApplicationDbContext.SeedOneExpenseItemAsync(owner, userExpenseItemType);

        var localizedUserExpenseItemType = await ApplicationDbContext.MetadataLocalizations
            .FirstAsync(x => x.MetadataTypeCode == Domain.Metadata.MetadataType.ExpenseItemType.Code
                             && x.SupportedLanguageCode == owner.PreferredLocalizationCode
                             && x.LocalizationKeyword == userExpenseItemType.LocalizationKeyword);

        var expectedResponse = new GetUserExpenseItemQueryResponse
        {
            Items =
            [
                new GetUserExpenseItemQueryResponseItem
                {
                    Id = userExpenseWithTags.Id,
                    Name = userExpenseWithTags.Name,
                    Color = userExpenseWithTags.Color,
                    ExpenseItemType = new GetUserExpenseItemQueryResponseItemTypeDto
                    {
                        Name = localizedUserExpenseItemType.LocalizedValue,
                        Code = userExpenseWithTags.ExpenseItemTypeCode!
                    },
                    Tags =
                    [
                        new GetUserExpenseItemQueryResponseTagDto
                        {
                            Id = tags[0].Id,
                            Name = tags[0].Name
                        }
                    ]
                },
                new GetUserExpenseItemQueryResponseItem
                {
                    Id = userExpenseItemWithoutTags.Id,
                    Name = userExpenseItemWithoutTags.Name,
                    Color = userExpenseItemWithoutTags.Color,
                    ExpenseItemType = new GetUserExpenseItemQueryResponseItemTypeDto
                    {
                        Name = localizedUserExpenseItemType.LocalizedValue,
                        Code = userExpenseItemWithoutTags.ExpenseItemTypeCode!
                    },
                    Tags = []
                }
            ]
        };
        expectedResponse.Items = expectedResponse.Items.OrderBy(x => x.Name).ToList();

        var request = new GetUserExpenseItemQueryRequest
        {
            OwnerId = owner.Id,
            LocalizationCode = owner.PreferredLocalizationCode
        };

        // Act
        var actualResponse = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.Equal(expectedResponse.AsJsonString(), actualResponse.AsJsonString());
    }

    [Fact]
    public async Task UserDoesNotExists()
    {
        // Arrange

        var request = new GetUserExpenseItemQueryRequest
        {
            OwnerId = -1,
            LocalizationCode = Guid.NewGuid().ToString()
        };
        var expectedResponse = new GetUserExpenseItemQueryResponse
        {
            Items = []
        };

        // Act
        var actualResponse = await _handler.Handle(request, CancellationToken.None);


        // Assert
        Assert.Equal(expectedResponse.AsJsonString(), actualResponse.AsJsonString());
    }


    [Fact]
    public async Task ExpenseItemTypeLocalizationDoesNotExists()
    {
        // Arrange
        var owner = await ApplicationDbContext.SeedOneUserAsync();
        var userExpenseItemType = await ApplicationDbContext.SeedOneExpenseItemTypeAsync(owner.PreferredLocalization);
        await ApplicationDbContext.SeedManyExpenseItemAsync(2, owner, userExpenseItemType);

        var expectedResponse = new GetUserExpenseItemQueryResponse
        {
            Items = []
        };
        expectedResponse.Items = expectedResponse.Items.OrderBy(x => x.Name).ToList();

        var request = new GetUserExpenseItemQueryRequest
        {
            OwnerId = owner.Id,
            LocalizationCode = Guid.NewGuid().ToString()
        };

        // Act
        var actualResponse = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.Equal(expectedResponse.AsJsonString(), actualResponse.AsJsonString());
    }
}