using System.Threading.Tasks;
using FinanceHelper.Application.Queries.SupportedLanguages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinanceHelper.Api.Controllers.V1;

/// <summary>
///     Supported languages controller
/// </summary>
[AllowAnonymous]
[Route("supported-languages")]
public class SupportedLanguagesController : ApiControllerBase
{
    /// <summary>
    ///     Get all supported languages
    /// </summary>
    /// <returns>All supported languages</returns>
    [HttpGet]
    [ProducesResponseType(typeof(GetLocalizedSupportedLanguagesQueryResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var command = new GetLocalizedSupportedLanguagesQueryRequest();

        var response = await Mediator.Send(command);

        return Ok(response);
    }
}