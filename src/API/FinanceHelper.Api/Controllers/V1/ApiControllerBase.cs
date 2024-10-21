using Asp.Versioning;
using FinanceHelper.Api.Services.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace FinanceHelper.Api.Controllers.V1;

/// <summary>
///     Base API controller version 1.0
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Produces("application/json")]
[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
public class ApiControllerBase : ControllerBase
{
    private ICurrentUserService? _currentUserService;
    private IMediator? _mediator;

    /// <summary>
    ///     Mediator instance in current HTTP request scope
    /// </summary>
    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>()!;

    /// <summary>
    ///     CurrentUserService instance in current HTTP request scope
    /// </summary>
    protected ICurrentUserService CurrentUserService => _currentUserService ??= HttpContext.RequestServices.GetService<ICurrentUserService>()!;
}