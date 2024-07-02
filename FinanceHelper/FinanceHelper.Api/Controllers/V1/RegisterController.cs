using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using FinanceHelper.Api.Contracts.Register;
using FinanceHelper.Application.Commands.Users.Register;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace FinanceHelper.Api.Controllers.V1;

/// <summary>
///     Registration controller
/// </summary>
[Route("register")]
public class RegisterController(IOptions<RequestLocalizationOptions> localizationOptions) : ApiControllerBase
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
        var preferredLocalizationCode = body.PreferredLocalizationCode?.ToLower();
        if (localizationOptions.Value.SupportedCultures!.Any(x => x.TwoLetterISOLanguageName == preferredLocalizationCode) == false) preferredLocalizationCode = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
        if (string.IsNullOrWhiteSpace(preferredLocalizationCode)) preferredLocalizationCode = localizationOptions.Value.DefaultRequestCulture.UICulture.TwoLetterISOLanguageName;

        var command = new RegisterUserCommandRequest
        {
            Email = body.Email.Trim().ToLower(),
            Password = body.Password.Trim(),
            PreferredLocalizationCode = preferredLocalizationCode
        };

        var response = await Mediator.Send(command);

        return Ok(response);
    }
}