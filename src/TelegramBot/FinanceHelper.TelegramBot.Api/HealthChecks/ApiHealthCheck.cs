using System.Reflection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace FinanceHelper.TelegramBot.Api.HealthChecks;

/// <summary>
///     HealthCheck
/// </summary>
public class ApiHealthCheck : IHealthCheck
{
    /// <summary>
    ///     Async healthckeck
    /// </summary>
    /// <param name="context">Health check context</param>
    /// <param name="cancellationToken">Cancellaction token</param>
    /// <returns>Health check result</returns>
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var versionNumber = assembly.GetName().Version;

        return Task.FromResult(HealthCheckResult.Healthy($"Build {versionNumber}"));
    }
}