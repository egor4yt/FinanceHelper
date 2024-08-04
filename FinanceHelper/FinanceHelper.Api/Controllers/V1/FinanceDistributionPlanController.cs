using System.Linq;
using System.Threading.Tasks;
using FinanceHelper.Api.Contracts.FinanceDistributionPlan;
using FinanceHelper.Application.Commands.FinanceDistributionPlans.Create;
using FinanceHelper.Application.Commands.Users.Update;
using FinanceHelper.Application.Queries.FinanceDistributionPlans.Details;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinanceHelper.Api.Controllers.V1;

/// <summary>
///     Finance distribution plan controller
/// </summary>
[Authorize]
[Route("finance-distribution-plan")]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
public class FinanceDistributionPlanController : ApiControllerBase
{
    /// <summary>
    ///     Create a finance distribution plan
    /// </summary>
    /// <returns>Created finance distribution plan</returns>
    [HttpPost("create")]
    [ProducesResponseType(typeof(CreateFinanceDistributionPlanCommandResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Create([FromBody] CreateFinanceDistributionPlanBody body)
    {
        var command = new CreateFinanceDistributionPlanCommandRequest
        {
            OwnerId = CurrentUserService.UserId,
            IncomeSourceId = body.IncomeSourceId,
            PlannedBudget = body.PlannedBudget,
            FactBudget = body.FactBudget,
            PlanItems = body.PlanItems.Select(x => new PlanItem
            {
                StepNumber = x.StepNumber,
                PlannedValue = x.PlannedValue,
                ExpenseItemId = x.ExpenseItemId
            }).ToList()
        };

        var response = await Mediator.Send(command);
        return Ok(response);
    }

    /// <summary>
    ///     Get the finance distribution plan details
    /// </summary>
    /// <returns>Finance distribution plan details</returns>
    [HttpGet("details/{planId:long}")]
    [ProducesResponseType(typeof(DetailsFinanceDistributionPlanQueryResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Details([FromRoute] long planId)
    {
        var query = new DetailsFinanceDistributionPlanQueryRequest
        {
            FinanceDistributionPlanId = planId,
            OwnerId = CurrentUserService.UserId
        };
        
        var response = await Mediator.Send(query);
        return Ok(response);
    }
}