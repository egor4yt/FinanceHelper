using System.Globalization;
using System.Numerics;

namespace FinanceHelper.Shared;

public static class StringHelper
{
    /// <summary>
    ///     Converts the <paramref name="number" /> to string with unified format settings
    /// </summary>
    /// <returns>Number as a string</returns>
    public static string ToUiString<T>(this T number) where T : IFloatingPoint<T>
    {
        var nfi = new NumberFormatInfo
        {
            NumberGroupSeparator = " ",
            NumberDecimalSeparator = ".",
            NumberGroupSizes = [3],
            NumberDecimalDigits = 2
        };

        var roundNumber = T.Round(number, 2);
        return roundNumber.ToString("N", nfi);
    }
}