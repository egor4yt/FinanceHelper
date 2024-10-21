namespace FinanceHelper.Api.Contracts.IncomeSource;

/// <summary>
///     Create income source body
/// </summary>
public class CreateIncomeSourceBody
{
    /// <summary>
    ///     Income source name
    /// </summary>
    public required string Name { get; init; } = string.Empty;

    /// <summary>
    ///     Income source color in HEX-format
    /// </summary>
    public required string Color { get; init; } = string.Empty;

    /// <summary>
    ///     Income source type code
    /// </summary>
    public required string IncomeSourceTypeCode { get; init; } = string.Empty;
}