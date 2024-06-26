using System.Security.Cryptography;
using System.Text;

namespace FinanceHelper.Shared;

public class SecurityHelper
{
    public static string ComputeSha256Hash(string rawData)
    {
        var inputBytes = Encoding.UTF8.GetBytes(rawData);
        var hashBytes = SHA256.HashData(inputBytes);

        return Convert.ToHexString(hashBytes);
    }
}