namespace Sekmen.Commerce.Services.Carts.Api.Extensions;

public static class SecurityExtensions
{
    private static readonly string[] LocalOrigins =
    [
        "http://localhost:3000",
        "http://localhost:3500",
        "http://*.sekmen.dev"
    ];
    private static readonly string[] ProdOrigins =
    [
        "http://*.sekmen.dev"
    ];
    public static IServiceCollection AddCors(this IServiceCollection services)
    {
        return services.AddCors(options =>
        {
            options.AddPolicy("development", x =>
            {
                x.AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()
                    .WithOrigins(LocalOrigins)
                    .SetIsOriginAllowedToAllowWildcardSubdomains();
            });
            options.AddPolicy("production", x =>
            {
                x.AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()
                    .WithOrigins(ProdOrigins)
                    .SetIsOriginAllowedToAllowWildcardSubdomains();
            });
        });
    }

    public static void UseHeaders(this IApplicationBuilder app)
    {
        app.Use(async (context, next) =>
        {
            context.Response.Headers.Remove("x-powered-by");
            context.Response.Headers.Remove("server");
            context.Response.Headers.Append("X-Frame-Options", "SAMEORIGIN");
            context.Response.Headers.Append("X-Content-Type-Options", "nosniff");
            context.Response.Headers.Append("Referrer-Policy", "no-referrer");
            await next();
        });
    }
}