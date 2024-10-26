namespace FinanceHelper.Api.Contracts.IncomeSource;

/// <summary>
///     Update income source body
/// </summary>
public class UpdateIncomeSourceBody
{
    /// <summary>
    ///     New income source name
    /// </summary>
    public string Name { get; init; } = string.Empty!;

    /// <summary>
    ///     New income source color
    /// </summary>
    public string Color { get; init; } = string.Empty!;

    /// <summary>
    ///     New income source type code
    /// </summary>
    public string IncomeSourceTypeCode { get; init; } = string.Empty!;
}