using System.Security.Cryptography;
using System.Text;
using RfidTraceability.Application.Security;

namespace RfidTraceability.Infrastructure.Security;

public sealed class RefreshTokenGenerator : IRefreshTokenGenerator
{
    public string Generate()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
    }

    public string Hash(string token)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(token));
        return Convert.ToHexString(bytes);
    }
}
