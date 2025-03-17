
using System.Text;

namespace BlockchainApp.Blockchain;

public static class Hasher
{
    public static string Sha256(string input)
    {
        using var sha256 = System.Security.Cryptography.SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(input);
        var hash = sha256.ComputeHash(bytes);
        
        var sb = new StringBuilder();

        for (int i = 0; i < hash.Length; i++)
        {
            sb.Append(hash[i].ToString("x2"));
        }
        
        return sb.ToString();
    }
}