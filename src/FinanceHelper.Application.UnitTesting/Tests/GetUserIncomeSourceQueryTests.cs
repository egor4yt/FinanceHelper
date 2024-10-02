using FinanceHelper.Application.Queries.IncomeSources.GetUser;
using FinanceHelper.Application.UnitTesting.Common;
using FinanceHelper.Application.UnitTesting.Generators;
using Microsoft.EntityFrameworkCore;

namespace FinanceHelper.Application.UnitTesting.Tests;

public class GetUserIncomeSourceQueryTests : TestBase<GetUserIncomeSourceQueryHandler>
{
    private readonly GetUserIncomeSourceQueryHandler _handler;

    public GetUserIncomeSourceQueryTests()
    {
        _handler = new GetUserIncomeSourceQueryHandler(ApplicationDbContext);
    }

    [Fact]
    public async Task Success()
    {
        // Arrange
        await ApplicationDbContext.SeedOneIncomeSourceAsync(); // Generate random income source

        var owner = await ApplicationDbContext.SeedOneUserAsync();
        var userIncomeSourceType = await ApplicationDbContext.SeedOneIncomeSourceTypeAsync(owner.PreferredLocalization);
        var userIncomeSources = await ApplicationDbContext.SeedManyIncomeSourceAsync(2, owner, userIncomeSourceType);

        var localizedUserIncomeSourceType = await ApplicationDbContext.MetadataLocalizations
            .FirstAsync(x => x.MetadataTypeCode == Domain.Metadata.MetadataType.IncomeSourceType.Code
                             && x.SupportedLanguageCode == owner.PreferredLocalizationCode
                             && x.LocalizationKeyword == userIncomeSourceType.LocalizationKeyword);

        var expectedResponse = new GetUserIncomeSourceQueryResponse
        {
            Items =
            [
                new GetUserIncomeSourceQueryResponseItem
                {
                    Id = userIncomeSources[0].Id,
                    Name = userIncomeSources[0].Name,
                    Color = userIncomeSources[0].Color,
                    IncomeSourceType = new GetUserIncomeSourceQueryResponseItemTypeDto
                    {
                        Name = localizedUserIncomeSourceType.LocalizedValue,
                        Code = userIncomeSources[0].IncomeSourceTypeCode
                    }
                },
                new GetUserIncomeSourceQueryResponseItem
                {
                    Id = userIncomeSources[1].Id,
                    Name = userIncomeSources[1].Name,
                    Color = userIncomeSources[1].Color,
                    IncomeSourceType = new GetUserIncomeSourceQueryResponseItemTypeDto
                    {
                        Name = localizedUserIncomeSourceType.LocalizedValue,
                        Code = userIncomeSources[1].IncomeSourceTypeCode
                    }
                }
            ]
        };
        expectedResponse.Items = expectedResponse.Items.OrderBy(x => x.Name).ToList();

        var request = new GetUserIncomeSourceQueryRequest
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

        var request = new GetUserIncomeSourceQueryRequest
        {
            OwnerId = -1,
            LocalizationCode = Guid.NewGuid().ToString()
        };
        var expectedResponse = new GetUserIncomeSourceQueryResponse
        {
            Items = []
        };

        // Act
        var actualResponse = await _handler.Handle(request, CancellationToken.None);


        // Assert
        Assert.Equal(expectedResponse.AsJsonString(), actualResponse.AsJsonString());
    }


    [Fact]
    public async Task IncomeSourceTypeLocalizationDoesNotExists()
    {
        // Arrange
        var owner = await ApplicationDbContext.SeedOneUserAsync();
        var userIncomeSourceType = await ApplicationDbContext.SeedOneIncomeSourceTypeAsync(owner.PreferredLocalization);
        await ApplicationDbContext.SeedManyIncomeSourceAsync(2, owner, userIncomeSourceType);

        var expectedResponse = new GetUserIncomeSourceQueryResponse
        {
            Items = []
        };
        expectedResponse.Items = expectedResponse.Items.OrderBy(x => x.Name).ToList();

        var request = new GetUserIncomeSourceQueryRequest
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