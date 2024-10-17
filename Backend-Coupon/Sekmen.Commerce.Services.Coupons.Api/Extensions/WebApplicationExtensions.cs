namespace Sekmen.Commerce.Services.Coupons.Api.Extensions;

public static class WebApplicationExtensions
{
    internal static void AddInternalDependencies(this WebApplicationBuilder builder)
    {
        var secret = builder.Configuration.GetValue<string>("JwtOptions:Secret")!;
        var issuer = builder.Configuration.GetValue<string>("JwtOptions:Issuer")!;
        var audience = builder.Configuration.GetValue<string>("JwtOptions:Audience")!;
        var key = Encoding.ASCII.GetBytes(secret);
        _ = builder.Services
            .AddAutoMapper(typeof(ICommand))
            .AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<ICommand>())
            .AddDbContext<CouponDbContext>(options => 
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
            )
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = issuer,
                    ValidateAudience = true,
                    ValidAudience = audience
                };
            });
        _ = builder.Services.AddAuthorization();
    }

    internal static void ApplyMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<CouponDbContext>();
        if (db.Database.GetPendingMigrations().Any())
        {
            db.Database.Migrate();
        }
    }
}