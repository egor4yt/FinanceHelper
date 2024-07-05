using System.Threading.Tasks;
using FinanceHelper.Api.Configuration.Options;
using FinanceHelper.Api.Contracts.User;
using FinanceHelper.Application.Commands.Users.Update;
using FinanceHelper.Application.Queries.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace FinanceHelper.Api.Controllers.V1;

/// <summary>
///     User controller
/// </summary>
[Authorize]
[Route("user")]
public class UserController(IOptions<JwtOptions> jwtOptions) : ApiControllerBase
{
    /// <summary>
    ///     Get user information
    /// </summary>
    /// <returns>Authorized user data</returns>
    [HttpGet("me")]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(GetOneUserQueryResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMe()
    {
        var command = new GetOneUserQueryRequest
        {
            Id = CurrentUserService.UserId
        };

        var response = await Mediator.Send(command);
        return Ok(response);
    }

    /// <summary>
    ///     Update user information
    /// </summary>
    /// <returns>new user data</returns>
    [HttpPut("me")]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(UpdateUserCommandResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateMe([FromBody] UpdateMeUserBody body)
    {
        var command = new UpdateUserCommandRequest
        {
            Id = CurrentUserService.UserId,
            Email = body.Email,
            PreferredLocalizationCode = body.PreferredLocalization,
            JwtDescriptorDetails = jwtOptions.Value.ToJwtDescriptorDetails()
        };

        var response = await Mediator.Send(command);
        return Ok(response);
    }
}