using System.Text.Json;

namespace FinanceHelper.Application.UnitTesting.Common;

public static class ObjectExtensions
{
    public static string AsJsonString(this object obj)
    {
        return JsonSerializer.Serialize(obj);
    }
}