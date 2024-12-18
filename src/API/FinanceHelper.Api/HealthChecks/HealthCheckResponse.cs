﻿using System.Collections.Generic;

namespace FinanceHelper.Api.HealthChecks;

/// <summary>
///     Describes state of the all services of the application
/// </summary>
public class HealthCheckResponse
{
    /// <summary>
    ///     Overall state of the application
    /// </summary>
    public string OverallStatus { get; set; } = string.Empty;

    /// <summary>
    ///     State of each service of the application
    /// </summary>
    public IEnumerable<HealthCheckItem> HealthChecks { get; set; } = [];

    /// <summary>
    ///     Total response time from all services
    /// </summary>
    public string TotalDuration { get; set; } = string.Empty;
}