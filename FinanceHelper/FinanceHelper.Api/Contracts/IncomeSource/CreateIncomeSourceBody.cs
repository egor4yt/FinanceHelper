namespace FinanceHelper.Api.Contracts.IncomeSource;

/// <summary>
///     Create income source body
/// </summary>
public class CreateIncomeSourceBody
{
    /// <summary>
    ///     Income source name
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Income source color in HEX-format
    /// </summary>
    public string Color { get; set; }

    /// <summary>
    ///     Income source type code
    /// </summary>
    public string IncomeSourceTypeCode { get; set; }
}