namespace Sekmen.Commerce.Carts.Api.Extensions;

public static class WebApplicationExtensions
{
    internal static void AddInternalDependencies(this WebApplicationBuilder builder)
    {
        builder.AddInternalAuthentication();
        builder.AddApiServices();
        builder.Services
            .AddAutoMapper(typeof(ICommand))
            .AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<ICommand>())
            .AddDbContext<CartDbContext>(options => 
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
            )
            .AddCors()
            .AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition(name: JwtBearerDefaults.AuthenticationScheme, securityScheme: new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = "Enter the Bearer Auth string: `Bearer jwt-generated-token`",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = JwtBearerDefaults.AuthenticationScheme
                            }
                        },
                        []
                    }
                });
            });
    }

    private static void AddApiServices(this WebApplicationBuilder builder)
    {
        builder.Services
			.AddHttpContextAccessor()
			.AddScoped<BackendApiAuthenticationHttpClientHandler>();

        var productUri = new Uri(builder.Configuration.GetValue<string>("Services:ProductApi:Url")!);
        builder.Services
            .AddScoped<IProductService, ProductService>()
            .AddHttpClient<IProductService, ProductService>(m => m.BaseAddress = productUri)
            .AddHttpMessageHandler<BackendApiAuthenticationHttpClientHandler>();

        var couponUri = new Uri(builder.Configuration.GetValue<string>("Services:CouponApi:Url")!);
        builder.Services
            .AddScoped<ICouponService, CouponService>()
            .AddHttpClient<ICouponService, CouponService>(m => m.BaseAddress = couponUri)
            .AddHttpMessageHandler<BackendApiAuthenticationHttpClientHandler>();
    }

    private static void AddInternalAuthentication(this WebApplicationBuilder builder)
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
        builder.Services.AddAuthorization();
    }

    internal static void ApplyMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<CartDbContext>();
        if (db.Database.GetPendingMigrations().Any())
        {
            db.Database.Migrate();
        }
    }
}