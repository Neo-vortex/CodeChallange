using System.Security.Cryptography;
using System.Text;

namespace CodeChallenge.Utilities;

public static class UtilityFunctions
{
    public static string CalculateHash(string read)
    {
        var crypt = SHA256.Create();
        var hash = string.Empty;
        var crypto = crypt.ComputeHash(Encoding.ASCII.GetBytes(read));
        return crypto.Aggregate(hash, (current, theByte) => current + theByte.ToString("x2"));
    }
}