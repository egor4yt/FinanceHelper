﻿using System.Globalization;
using System.Threading.Tasks;
using FinanceHelper.Api.Contracts.ExpenseItem;
using FinanceHelper.Application.Commands.ExpenseItems.Create;
using FinanceHelper.Application.Commands.ExpenseItems.Delete;
using FinanceHelper.Application.Commands.ExpenseItems.Update;
using FinanceHelper.Application.Commands.Tags.Attach;
using FinanceHelper.Application.Queries.ExpenseItems.GetUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinanceHelper.Api.Controllers.V1;

/// <summary>
///     expense items controller
/// </summary>
[Authorize]
[Route("expense-item")]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
public class ExpenseItemController : ApiControllerBase
{
    /// <summary>
    ///     Create an expense item for authorized user
    /// </summary>
    /// <returns>Created expense item</returns>
    [HttpPost("create")]
    [ProducesResponseType(typeof(CreateExpenseItemCommandResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Create([FromBody] CreateExpenseItemBody body)
    {
        var command = new CreateExpenseItemCommandRequest
        {
            Name = body.Name,
            Color = body.Color,
            ExpenseItemTypeCode = body.ExpenseItemTypeCode,
            OwnerId = CurrentUserService.UserId
        };

        var response = await Mediator.Send(command);
        return Ok(response);
    }

    /// <summary>
    ///     Attach tag to expense item for authorized user
    /// </summary>
    [HttpPost("{expenseItemId:long}/attach-tag/{tagId:long}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> AttachTag([FromRoute] long expenseItemId, [FromRoute] long tagId)
    {
        var query = new AttachTagCommandRequest
        {
            TagId = tagId,
            EntityId = expenseItemId,
            OwnerId = CurrentUserService.UserId,
            TagTypeCode = Domain.Metadata.TagType.ExpenseItem.Code
        };

        await Mediator.Send(query);
        return NoContent();
    }

    /// <summary>
    ///     Get an authorized user's expense items
    /// </summary>
    /// <returns>Expense items</returns>
    [HttpGet("my")]
    [ProducesResponseType(typeof(GetUserExpenseItemQueryResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUserExpenseItems()
    {
        var query = new GetUserExpenseItemQueryRequest
        {
            OwnerId = CurrentUserService.UserId,
            LocalizationCode = CultureInfo.CurrentCulture.TwoLetterISOLanguageName
        };

        var response = await Mediator.Send(query);
        return Ok(response);
    }

    /// <summary>
    ///     Delete an authorized user's expense item
    /// </summary>
    /// <param name="expenseItemId">Expense item id</param>
    [HttpDelete("{expenseItemId:long}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteUserExpenseItem([FromRoute] long expenseItemId)
    {
        var request = new DeleteExpenseItemCommandRequest
        {
            OwnerId = CurrentUserService.UserId,
            ExpenseItemId = expenseItemId
        };

        await Mediator.Send(request);
        return NoContent();
    }

    /// <summary>
    ///     Update an authorized user's expense item
    /// </summary>
    /// <param name="expenseItemId">Expense item id</param>
    /// <param name="body">New expense item information</param>
    [HttpPut("{expenseItemId:long}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateUserExpenseItem([FromRoute] long expenseItemId, [FromBody] UpdateExpenseItemBody body)
    {
        var request = new UpdateExpenseItemCommandRequest
        {
            OwnerId = CurrentUserService.UserId,
            ExpenseItemId = expenseItemId,
            Name = body.Name,
            Color = body.Color,
            ExpenseItemTypeCode = body.ExpenseItemTypeCode
        };

        await Mediator.Send(request);
        return NoContent();
    }
}