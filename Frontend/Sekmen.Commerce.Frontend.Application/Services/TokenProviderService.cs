using Microsoft.AspNetCore.Http;

namespace Sekmen.Commerce.Frontend.Application.Services;

public interface ITokenProviderService
{
    void ClearToken();
    string? GetToken();
    void SetToken(string token);
}

public class TokenProviderService(
    IHttpContextAccessor contextAccessor
) : ITokenProviderService
{
    public void ClearToken()
    {
        contextAccessor.HttpContext?.Response.Cookies.Delete(AuthConstants.TokenCookie);
    }

    public string? GetToken()
    {
        string? token = null;
        var hasToken = contextAccessor.HttpContext?.Request.Cookies.TryGetValue(AuthConstants.TokenCookie, out token);

        return hasToken is true
            ? token
            : null;
    }

    public void SetToken(string token)
    {
        contextAccessor.HttpContext?.Response.Cookies.Append(AuthConstants.TokenCookie, token);
    }
}