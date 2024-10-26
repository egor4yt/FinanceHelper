using System.Globalization;
using System.Threading.Tasks;
using FinanceHelper.Application.Queries.IncomeSourceTypes.GetLocalized;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinanceHelper.Api.Controllers.V1;

/// <summary>
///     Income sources types controller
/// </summary>
[Route("income-source-type")]
public class IncomeSourceTypeController : ApiControllerBase
{
    /// <summary>
    ///     Get localized income sources types
    /// </summary>
    /// <returns>Localized income sources types</returns>
    [HttpGet]
    [ProducesResponseType(typeof(GetLocalizedIncomeSourceTypesQueryResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get()
    {
        var query = new GetLocalizedIncomeSourceTypesQueryRequest
        {
            LanguageCode = CultureInfo.CurrentCulture.TwoLetterISOLanguageName
        };

        var response = await Mediator.Send(query);
        return Ok(response);
    }
}