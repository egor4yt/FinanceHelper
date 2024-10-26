using System.Globalization;
using System.Threading.Tasks;
using FinanceHelper.Application.Queries.ExpenseItemTypes.GetLocalized;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinanceHelper.Api.Controllers.V1;

/// <summary>
///     Expense items types controller
/// </summary>
[Route("expense-item-type")]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
public class ExpenseItemTypeController : ApiControllerBase
{
    /// <summary>
    ///     Get localized expense items types
    /// </summary>
    /// <returns>Localized expense items types</returns>
    [HttpGet]
    [ProducesResponseType(typeof(GetLocalizedExpenseItemTypesQueryResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get()
    {
        var query = new GetLocalizedExpenseItemTypesQueryRequest
        {
            LanguageCode = CultureInfo.CurrentCulture.TwoLetterISOLanguageName
        };

        var response = await Mediator.Send(query);
        return Ok(response);
    }
}