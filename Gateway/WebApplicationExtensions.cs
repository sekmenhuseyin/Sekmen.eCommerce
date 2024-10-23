namespace Sekmen.Commerce.Gateway;

public static class WebApplicationExtensions
{
    internal static void AddInternalAuthentication(this WebApplicationBuilder builder)
    {
        var secret = builder.Configuration.GetValue<string>("JwtOptions:Secret")!;
        var issuer = builder.Configuration.GetValue<string>("JwtOptions:Issuer")!;
        var audience = builder.Configuration.GetValue<string>("JwtOptions:Audience")!;
        var key = Encoding.ASCII.GetBytes(secret);
        builder.Services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.IncludeErrorDetails = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidIssuer = issuer,
                    ValidAudience = audience
                };
                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        var userId = context.Principal?.Claims.FirstOrDefault(y => y.Type == ClaimTypes.NameIdentifier)?.Value;
                        if (string.IsNullOrWhiteSpace(userId))
                            context.Fail("Unauthorized");

                        return Task.CompletedTask;
                    }
                };
            });
        builder.Services
            .AddAuthorizationBuilder()
            .AddPolicy("authenticated", policy => policy.RequireAuthenticatedUser());
    }
}