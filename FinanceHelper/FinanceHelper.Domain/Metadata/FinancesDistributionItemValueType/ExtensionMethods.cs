namespace FinanceHelper.Domain.Metadata;

public static partial class FinancesDistributionItemValueType
{
    public static Entities.FinancesDistributionItemValueType GetTypeFromStringValue(string valueAsString)
    {
        if (string.IsNullOrWhiteSpace(valueAsString)) throw new ArgumentException($"{nameof(valueAsString)} was null or white space");
        return valueAsString.Last() == '%' ? Floating : Fixed;
    }

    public static decimal GetValueFromStringValue(string valueAsString)
    {
        if (string.IsNullOrWhiteSpace(valueAsString)) throw new ArgumentException($"{nameof(valueAsString)} was null or white space");

        var type = GetTypeFromStringValue(valueAsString);
        if (type.Code == Fixed.Code) return decimal.Parse(valueAsString);
        if (type.Code == Floating.Code) return decimal.Parse(valueAsString[..^1]);

        throw new InvalidOperationException($"{type.Code} does not allowed");
    }
}