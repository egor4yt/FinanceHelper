using FinanceHelper.TelegramBot.Api.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;

namespace FinanceHelper.TelegramBot.Api.Services;

/// <summary>
///     HealthCheck service
/// </summary>
public abstract class HealthCheckService
{
    /// <summary>
    ///     Generate healthcheck report
    /// </summary>
    /// <param name="httpContext">Http context</param>
    /// <param name="report">Report</param>
    public static async Task WriterHealthCheckResponse(HttpContext httpContext, HealthReport report)
    {
        httpContext.Response.ContentType = "application/json";
        var response = new HealthCheckResponse
        {
            OverallStatus = report.Status.ToString(),
            TotalDuration = report.TotalDuration.TotalSeconds.ToString("0:0.00"),
            HealthChecks = report.Entries.Select(x => new HealthCheckItem
            {
                Status = x.Value.Status.ToString(),
                Component = x.Key,
                Description = x.Value.Description ?? "",
                Duration = x.Value.Duration.TotalSeconds.ToString("0:0.00")
            })
        };

        await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(response, Formatting.Indented));
    }
}