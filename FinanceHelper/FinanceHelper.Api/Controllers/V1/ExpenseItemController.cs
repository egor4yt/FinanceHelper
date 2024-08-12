using System.Threading.Tasks;
using FinanceHelper.Api.Contracts.ExpenseItem;
using FinanceHelper.Application.Commands.ExpenseItems.Create;
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
    ///     Create an expense item
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
}