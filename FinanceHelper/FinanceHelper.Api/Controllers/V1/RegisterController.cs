using System.Globalization;
using System.Threading.Tasks;
using FinanceHelper.Api.Contracts.Register;
using FinanceHelper.Application.Commands.Users.Register;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinanceHelper.Api.Controllers.V1;

/// <summary>
///     Registration controller
/// </summary>
[Route("register")]
public class RegisterController : ApiControllerBase
{
    /// <summary>
    ///     Register user
    /// </summary>
    /// <param name="body">Registration user data</param>
    /// <returns>Registered user data</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(RegisterUserCommandResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Register([FromBody] RegisterUserBody body)
    {
        var command = new RegisterUserCommandRequest
        {
            Email = body.Email.Trim().ToLower(),
            Password = body.Password.Trim()
        };

        var response = await Mediator.Send(command);

        return Ok(response);
    }
}