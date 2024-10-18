namespace Sekmen.Commerce.Services.Auth.Api.Filters;

public class ApiAuthenticationFilter(
    IHttpContextAccessor httpContextAccessor
) : IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (string.IsNullOrWhiteSpace(httpContextAccessor.HttpContext?.Request.Headers.Authorization.ToString()))
        {
            context.Result = new UnauthorizedResult();
        }
    }
}