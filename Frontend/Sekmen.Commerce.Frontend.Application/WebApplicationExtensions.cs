namespace Sekmen.Commerce.Frontend.Application;

public static class WebApplicationExtensions
{
    public static void AddInternalDependencies(this WebApplicationBuilder builder)
    {
        _ = builder.Services
            .AddSingleton(_ => builder.Configuration.Get<AppSettingsModel>()!)
            .AddHttpContextAccessor()
            .AddHttpClient()
            .AddScoped<BaseService>()
            .AddScoped<ITokenProviderService, TokenProviderService>()
            .AddScoped<ICouponService, CouponService>()
            .AddScoped<IAuthService, AuthService>();
        _ = builder.Services.AddHttpClient<ICouponService, CouponService>();
        _ = builder.Services.AddHttpClient<IAuthService, AuthService>();
        _ = builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromHours(24);
                options.LoginPath = "/auth/login";
                options.AccessDeniedPath = "/auth/access-denied";
            });
    }
}