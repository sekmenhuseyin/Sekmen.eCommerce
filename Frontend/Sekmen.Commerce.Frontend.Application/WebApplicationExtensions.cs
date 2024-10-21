namespace Sekmen.Commerce.Frontend.Application;

public static class WebApplicationExtensions
{
    public static void AddInternalDependencies(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddSingleton(_ => builder.Configuration.Get<AppSettingsModel>()!)
            .AddHttpContextAccessor()
            .AddHttpClient()
            .AddScoped<BaseService>()
            .AddScoped<ITokenProviderService, TokenProviderService>()
            .AddScoped<IAuthService, AuthService>()
            .AddScoped<ICouponService, CouponService>()
            .AddScoped<IProductService, ProductService>();
        builder.Services.AddHttpClient<IAuthService, AuthService>();
        builder.Services.AddHttpClient<ICouponService, CouponService>();
        builder.Services.AddHttpClient<IProductService, ProductService>();
        builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromHours(24);
                options.LoginPath = "/auth/login";
                options.AccessDeniedPath = "/auth/access-denied";
            });
    }
}