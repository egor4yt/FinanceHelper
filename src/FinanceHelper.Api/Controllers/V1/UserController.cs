using System.Threading.Tasks;
using FinanceHelper.Api.Configuration.Options;
using FinanceHelper.Api.Contracts.User;
using FinanceHelper.Application.Commands.Users.Update;
using FinanceHelper.Application.Queries.Users.GetOne;
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
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
public class UserController(IOptions<JwtOptions> jwtOptions) : ApiControllerBase
{
    /// <summary>
    ///     Get authorized user information
    /// </summary>
    /// <returns>Authorized user information</returns>
    [HttpGet("me")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(GetOneUserQueryResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMe()
    {
        var query = new GetOneUserQueryRequest
        {
            Id = CurrentUserService.UserId
        };

        var response = await Mediator.Send(query);
        return Ok(response);
    }

    /// <summary>
    ///     Update authorized user information
    /// </summary>
    /// <returns>Updated user information and a new JSON web token</returns>
    [HttpPut("me")]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(UpdateUserCommandResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateMe([FromBody] UpdateMeUserBody body)
    {
        var command = new UpdateUserCommandRequest
        {
            Id = CurrentUserService.UserId,
            Email = body.Email,
            PreferredLocalizationCode = body.PreferredLocalization,
            JwtDescriptorDetails = jwtOptions.Value.ToJwtDescriptorDetails(),
            FirstName = body.FirstName,
            LastName = body.LastName
        };

        var response = await Mediator.Send(command);
        return Ok(response);
    }
}