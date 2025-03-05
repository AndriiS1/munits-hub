using System.Security.Cryptography;
using System.Text;
namespace MunitSHub.UseCases.Services.HashService;

public class HashService : IHashService
{
    public string GetHash(string text)
    {
        var hashedBytes = SHA256.HashData(Encoding.UTF8.GetBytes(text));
        return Convert.ToHexStringLower(hashedBytes);
    }
}
