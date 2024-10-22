namespace Sekmen.Commerce.Services.Auth.Api.Extensions;

public static class HttpContextExtensions
{
    public static string? GetUserId(this HttpContext? context)
    {
        return context == null ? null : GetUserId(context.User.Claims);
    }

    public static string? GetUserId(this IEnumerable<Claim> claims)
    {
        return claims.FirstOrDefault(y => y.Type == ClaimTypes.NameIdentifier)?.Value;
    }
}