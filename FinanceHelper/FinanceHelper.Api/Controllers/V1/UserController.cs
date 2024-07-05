using System.Threading.Tasks;
using FinanceHelper.Application.Commands.Users.Register;
using FinanceHelper.Application.Queries.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinanceHelper.Api.Controllers.V1;

/// <summary>
///     User controller
/// </summary>
[Authorize]
[Route("user")]
public class UserController : ApiControllerBase
{
    /// <summary>
    ///     Get information about authorized user
    /// </summary>
    /// <returns>Authorized user data</returns>
    [HttpGet("me")]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(RegisterUserCommandResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMe()
    {
        var command = new GetOneUserQueryRequest
        {
            Id = CurrentUserService.UserId
        };

        var response = await Mediator.Send(command);
        return Ok(response);
    }
}