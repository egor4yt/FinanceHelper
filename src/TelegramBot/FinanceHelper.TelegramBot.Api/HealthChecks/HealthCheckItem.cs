namespace FinanceHelper.TelegramBot.Api.HealthChecks;

/// <summary>
///     Describes state of the single service of the application
/// </summary>
public class HealthCheckItem
{
    /// <summary>
    ///     Status of the service
    /// </summary>
    public string Status { get; set; } = string.Empty;

    /// <summary>
    ///     Service name
    /// </summary>
    public string Component { get; set; } = string.Empty;

    /// <summary>
    ///     Description of the service state
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    ///     Response time from service
    /// </summary>
    public string Duration { get; set; } = string.Empty;
}