using System.Linq;
using System.Threading.Tasks;
using FinanceHelper.Api.Contracts.FinanceDistributionPlanTemplate;
using FinanceHelper.Application.Commands.FinanceDistributionPlanTemplates.Create;
using FinanceHelper.Application.Queries.FinanceDistributionPlansTemplates.GetUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinanceHelper.Api.Controllers.V1;

/// <summary>
///     Finance distribution plan templates controller
/// </summary>
[Authorize]
[Route("finance-distribution-plan-template")]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
public class FinanceDistributionPlanTemplateController : ApiControllerBase
{
    /// <summary>
    ///     Create a finance distribution plan template
    /// </summary>
    /// <returns>Created finance distribution plan template</returns>
    [HttpPost("create")]
    [ProducesResponseType(typeof(CreateFinanceDistributionPlanTemplateCommandResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Create([FromBody] CreateFinanceDistributionPlanTemplateBody body)
    {
        var command = new CreateFinanceDistributionPlanTemplateCommandRequest
        {
            OwnerId = CurrentUserService.UserId,
            IncomeSourceId = body.IncomeSourceId,
            PlannedBudget = body.PlannedBudget,
            Name = body.Name,
            FixedPlanItems = body.FixedPlanItems?.Select(x => new FixedPlanTemplateItem
            {
                PlannedValue = x.PlannedValue,
                Id = x.ExpenseItemId,
                Indivisible = x.Indivisible
            }).ToList(),
            FloatingPlanItems = body.FloatingPlanItems.Select(x => new FloatingPlanTemplateItem
            {
                PlannedValue = x.PlannedValue,
                Id = x.ExpenseItemId
            }).ToList()
        };

        var response = await Mediator.Send(command);
        return Ok(response);
    }

    /// <summary>
    ///     Get all user finance distribution plans templates
    /// </summary>
    /// <returns>Finance distribution plans templates</returns>
    [HttpGet("my")]
    [ProducesResponseType(typeof(GetUserFinanceDistributionPlanTemplateQueryResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUserPlans()
    {
        var query = new GetUserFinanceDistributionPlanTemplateQueryRequest
        {
            OwnerId = CurrentUserService.UserId
        };

        var response = await Mediator.Send(query);
        return Ok(response);
    }
}