using FinanceHelper.Application.Queries.FinanceDistributionPlans.GetUser;
using FinanceHelper.Application.UnitTesting.Common;
using FinanceHelper.Application.UnitTesting.Generators;

namespace FinanceHelper.Application.UnitTesting.Tests;

public class GetUserFinanceDistributionPlanQueryTests : TestBase<GetUserFinanceDistributionPlanQueryHandler>
{
    private readonly GetUserFinanceDistributionPlanQueryHandler _handler;

    public GetUserFinanceDistributionPlanQueryTests()
    {
        _handler = new GetUserFinanceDistributionPlanQueryHandler(ApplicationDbContext);
    }

    [Fact]
    public async Task Success()
    {
        // Arrange
        var user = await ApplicationDbContext.SeedOneUserAsync();
        var incomeSource1 = await ApplicationDbContext.SeedOneIncomeSourceAsync(user);
        var incomeSource2 = await ApplicationDbContext.SeedOneIncomeSourceAsync(user);
        var plan1 = await ApplicationDbContext.SeedOneFinanceDistributionPlanAsync(user, incomeSource1);
        var plan2 = await ApplicationDbContext.SeedOneFinanceDistributionPlanAsync(user, incomeSource2);

        var expectedResponse = new GetUserFinanceDistributionPlanQueryResponse
        {
            Items =
            [
                new GetUserFinanceDistributionPlanQueryResponseItem
                {
                    PlanId = plan2.Id,
                    PlannedBudget = Math.Round(plan2.PlannedBudget, 2),
                    FactBudget = Math.Round(plan2.FactBudget, 2),
                    CreatedAt = plan2.CreatedAt,
                    IncomeSourceName = incomeSource2.Name
                },
                new GetUserFinanceDistributionPlanQueryResponseItem
                {
                    PlanId = plan1.Id,
                    PlannedBudget = Math.Round(plan1.PlannedBudget, 2),
                    FactBudget = Math.Round(plan1.FactBudget, 2),
                    CreatedAt = plan1.CreatedAt,
                    IncomeSourceName = incomeSource1.Name
                }
            ]
        };
        var request = new GetUserFinanceDistributionPlanQueryRequest
        {
            OwnerId = user.Id
        };

        // Act
        var actualResponse = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.Multiple(
            () => Assert.NotNull(actualResponse),
            () => Assert.NotNull(actualResponse.Items),
            () => Assert.Collection(actualResponse.Items,
                first =>
                {
                    var expected = expectedResponse.Items[0];
                    Assert.Multiple(
                        () => Assert.Equal(first.CreatedAt, expected.CreatedAt),
                        () => Assert.Equal(first.FactBudget, expected.FactBudget),
                        () => Assert.Equal(first.PlanId, expected.PlanId),
                        () => Assert.Equal(first.IncomeSourceName, expected.IncomeSourceName),
                        () => Assert.Equal(first.PlannedBudget, expected.PlannedBudget)
                    );
                },
                second =>
                {
                    var expected = expectedResponse.Items[1];
                    Assert.Multiple(
                        () => Assert.Equal(second.CreatedAt, expected.CreatedAt),
                        () => Assert.Equal(second.FactBudget, expected.FactBudget),
                        () => Assert.Equal(second.PlanId, expected.PlanId),
                        () => Assert.Equal(second.IncomeSourceName, expected.IncomeSourceName),
                        () => Assert.Equal(second.PlannedBudget, expected.PlannedBudget)
                    );
                })
        );
    }

    [Fact]
    public async Task UserDoesNotExists()
    {
        // Arrange
        var request = new GetUserFinanceDistributionPlanQueryRequest
        {
            OwnerId = -1
        };

        // Act
        var response = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.Multiple(
            () => Assert.NotNull(response),
            () => Assert.NotNull(response.Items),
            () => Assert.Empty(response.Items)
        );
    }
}