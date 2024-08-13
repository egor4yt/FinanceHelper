using System.Globalization;
using System.Threading.Tasks;
using FinanceHelper.Api.Contracts.IncomeSource;
using FinanceHelper.Application.Commands.IncomeSources.Create;
using FinanceHelper.Application.Queries.IncomeSources.GetUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinanceHelper.Api.Controllers.V1;

/// <summary>
///     Income sources controller
/// </summary>
[Authorize]
[Route("income-source")]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
public class IncomeSourceController : ApiControllerBase
{
    /// <summary>
    ///     Create an income source
    /// </summary>
    /// <returns>Created income source</returns>
    [HttpPost("create")]
    [ProducesResponseType(typeof(CreateIncomeSourceCommandResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Create([FromBody] CreateIncomeSourceBody body)
    {
        var command = new CreateIncomeSourceCommandRequest
        {
            Name = body.Name,
            Color = body.Color,
            IncomeSourceTypeCode = body.IncomeSourceTypeCode,
            OwnerId = CurrentUserService.UserId
        };

        var response = await Mediator.Send(command);
        return Ok(response);
    }

    /// <summary>
    ///     Get an authorized user's income sources
    /// </summary>
    /// <returns>Income sources</returns>
    [HttpGet("my")]
    [ProducesResponseType(typeof(GetUserIncomeSourceQueryResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUserIncomeSources()
    {
        var query = new GetUserIncomeSourceQueryRequest
        {
            OwnerId = CurrentUserService.UserId,
            LocalizationCode = CultureInfo.CurrentCulture.TwoLetterISOLanguageName
        };

        var response = await Mediator.Send(query);
        return Ok(response);
    }
}