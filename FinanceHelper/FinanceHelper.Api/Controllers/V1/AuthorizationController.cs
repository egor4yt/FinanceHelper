using System.Threading.Tasks;
using FinanceHelper.Api.Configuration.Options;
using FinanceHelper.Api.Contracts.Authorization;
using FinanceHelper.Application.Commands.Authorize.WithCredentials;
using FinanceHelper.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace FinanceHelper.Api.Controllers.V1;

/// <summary>
///     Authorization controller
/// </summary>
[AllowAnonymous]
[Route("Authorization")]
public class AuthorizationController(IOptions<JwtOptions> jwtOptions) : ApiControllerBase
{
    /// <summary>
    ///     Authorize with user credentials
    /// </summary>
    /// <param name="body">User credentials</param>
    /// <returns>JSON web token</returns>
    [HttpPost("credentials")]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(AuthorizeWithCredentialsCommandResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Credentials([FromBody] AuthorizationLoginBody body)
    {
        var command = new AuthorizeWithCredentialsCommandRequest
        {
            Email = body.Email.Trim().ToLower(),
            PasswordHash = SecurityHelper.ComputeSha256Hash(body.Password.Trim()),
            JwtDescriptorDetails = jwtOptions.Value.ToJwtDescriptorDetails()
        };

        var response = await Mediator.Send(command);
        return Ok(response);
    }
}