﻿using System.Linq;
using System.Threading.Tasks;
using FinanceHelper.Api.Contracts.FinanceDistributionPlan;
using FinanceHelper.Application.Commands.FinanceDistributionPlans.Create;
using FinanceHelper.Application.Commands.Users.Update;
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
    [ProducesResponseType(typeof(UpdateUserCommandResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Create([FromBody] CreateFinanceDistributionPlanBody body)
    {
        var command = new CreateFinanceDistributionPlanCommandRequest
        {
            OwnerId = CurrentUserService.UserId,
            PlannedBudget = body.PlannedBudget,
            FactBudget = body.FactBudget,
            PlanItems = body.PlanItems.Select(x => new PlanItem
            {
                StepNumber = x.StepNumber,
                PlannedValue = x.PlannedValue,
                ExpenseItemId = x.ExpenseItemId,
                ValueTypeCode = x.ValueTypeCode
            }).ToList()
        };

        var response = await Mediator.Send(command);
        return Ok(response);
    }
}