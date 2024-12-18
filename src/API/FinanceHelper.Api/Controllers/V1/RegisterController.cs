﻿using System.Globalization;
using System.Threading.Tasks;
using FinanceHelper.Api.Configuration.Options;
using FinanceHelper.Api.Contracts.Register;
using FinanceHelper.Application.Commands.Users.Register;
using FinanceHelper.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace FinanceHelper.Api.Controllers.V1;

/// <summary>
///     Registration controller
/// </summary>
[AllowAnonymous]
[Route("register")]
public class RegisterController(IOptions<JwtOptions> jwtOptions) : ApiControllerBase
{
    /// <summary>
    ///     User registration
    /// </summary>
    /// <param name="body">Registration user data</param>
    /// <returns>Registered user data and a JSON web token</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(RegisterUserCommandResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Register([FromBody] RegisterUserBody body)
    {
        var command = new RegisterUserCommandRequest
        {
            Email = body.Email.Trim().ToLower(),
            PasswordHash = SecurityHelper.ComputeSha256Hash(body.Password.Trim()),
            PreferredLocalizationCode = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName, // From localization header
            JwtDescriptorDetails = jwtOptions.Value.ToJwtDescriptorDetails(),
            FirstName = body.FirstName,
            LastName = body.LastName
        };

        var response = await Mediator.Send(command);

        return Ok(response);
    }
}