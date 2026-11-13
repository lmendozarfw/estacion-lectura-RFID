namespace RfidTraceability.Application.Security;

public interface IRefreshTokenGenerator
{
    string Generate();

    string Hash(string token);
}
