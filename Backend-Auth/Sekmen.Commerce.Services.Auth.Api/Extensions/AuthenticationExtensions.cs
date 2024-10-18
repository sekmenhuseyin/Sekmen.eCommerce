namespace Sekmen.Commerce.Services.Auth.Api.Extensions;

public static class AuthenticationExtensions
{
    public static int GetUserId(this IHttpContextAccessor accessor)
    {
        return GetUserId(accessor.HttpContext);
    }

    public static int GetUserId(this HttpContext? context)
    {
        return context == null ? -1 : GetUserId(context.User.Claims);
    }

    public static int GetUserId(this IEnumerable<Claim> claims)
    {
        return Convert.ToInt32(claims.FirstOrDefault(y => y.Type == "id")?.Value);
    }
}