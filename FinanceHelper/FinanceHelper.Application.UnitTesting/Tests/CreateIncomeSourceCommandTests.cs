﻿using FinanceHelper.Application.Commands.IncomeSources.Create;
using FinanceHelper.Application.Exceptions;
using FinanceHelper.Application.UnitTesting.Common;
using Microsoft.EntityFrameworkCore;

namespace FinanceHelper.Application.UnitTesting.Tests;

public class CreateIncomeSourceCommandTests : TestBase<CreateIncomeSourceCommandHandler>
{
    private readonly CreateIncomeSourceCommandHandler _handler;

    public CreateIncomeSourceCommandTests()
    {
        _handler = new CreateIncomeSourceCommandHandler(ApplicationDbContext, Localizer);
    }

    [Fact]
    public async Task Success()
    {
        // Arrange
        var incomeSourceType = await IncomeSourceTypeGenerator.SeedOneAsync();
        var request = new CreateIncomeSourceCommandRequest
        {
            OwnerId = new Random().Next(),
            Name = Guid.NewGuid().ToString(),
            Color = Guid.NewGuid().ToString(),
            IncomeSourceTypeCode = incomeSourceType.Code
        };

        // Act
        var response = await _handler.Handle(request, CancellationToken.None);
        var databaseObject = await ApplicationDbContext.IncomeSources
            .SingleOrDefaultAsync(x => x.Id == response.Id
                                       && x.IncomeSourceTypeCode == request.IncomeSourceTypeCode
                                       && x.Color == request.Color
                                       && x.OwnerId == request.OwnerId
                                       && x.Name == request.Name);

        // Assert
        Assert.NotNull(databaseObject);
    }

    [Fact]
    public async Task InvalidIncomeSourceTypeCode()
    {
        // Arrange
        var request = new CreateIncomeSourceCommandRequest
        {
            OwnerId = new Random().Next(),
            Name = Guid.NewGuid().ToString(),
            Color = Guid.NewGuid().ToString(),
            IncomeSourceTypeCode = Guid.NewGuid().ToString()
        };

        // Assert
        await Assert.ThrowsAsync<BadRequestException>(async () => await _handler.Handle(request, CancellationToken.None));
    }
}