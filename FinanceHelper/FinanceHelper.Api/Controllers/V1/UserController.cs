using System.Threading.Tasks;
using FinanceHelper.Application.Commands.Users.Register;
using FinanceHelper.Application.Exceptions;
using FinanceHelper.Application.Queries.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinanceHelper.Api.Controllers.V1;

/// <summary>
///     User controller
/// </summary>
[Route("user")]
[Authorize]
public class UserController : ApiControllerBase
{
    /// <summary>
    ///     Get one user
    /// </summary>
    /// <param name="id">User id</param>
    /// <returns>User data</returns>
    [HttpGet("{id:long}")]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(RegisterUserCommandResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUser([FromRoute] long id)
    {
        var command = new GetOneUserQueryRequest
        {
            Id = id
        };

        if (id != CurrentUserService.UserId) throw new ForbiddenException("It is not your account");

        var response = await Mediator.Send(command);
        return Ok(response);
    }
}