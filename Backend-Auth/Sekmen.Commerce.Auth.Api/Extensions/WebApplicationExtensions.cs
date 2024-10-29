// ReSharper disable InvertIf
namespace Sekmen.Commerce.Auth.Api.Extensions;

public static class WebApplicationExtensions
{
    internal static void AddInternalDependencies(this WebApplicationBuilder builder)
    {
        builder.AddInternalAuthentication();
        builder.AddIdentity();
        builder.AddBus();
        builder.Services
            .AddSingleton(_ => builder.Configuration.Get<AppSettingsModel>()!)
            .AddScoped<IJwtTokenGenerator, JwtTokenGenerator>()
            .AddAutoMapper(typeof(ICommand))
            .AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<JwtTokenGenerator>())
            .AddCors()
            .AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition(name: JwtBearerDefaults.AuthenticationScheme, securityScheme: new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = "Enter the Bearer Auth string: `Bearer jwt-generated-token`",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = JwtBearerDefaults.AuthenticationScheme
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
            })
            .AddDbContext<AuthDbContext>(options => 
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
            );
    }

    private static void AddBus(this WebApplicationBuilder builder)
    {
        var busSetting = builder.Configuration.GetSection(RabbitMqSettings.SettingPath).Get<RabbitMqSettings>()!;
        if (busSetting.Disabled)
            return;

        _ = builder.Services.AddCap(x =>
        {
            x.UseEntityFramework<AuthDbContext>();
            x.UseRabbitMQ(options =>
            {
                options.HostName = busSetting.Connection;
                options.UserName = busSetting.Username;
                options.Password = busSetting.Password;
            });
            x.DefaultGroupName = busSetting.QueueName;
            x.TopicNamePrefix = busSetting.TopicPrefix;
        });
    }

    private static void AddIdentity(this WebApplicationBuilder builder)
    {
        var passwordOptions = builder.Configuration.GetRequiredSection("PasswordOptions").Get<PasswordOptionsModel>();
        builder.Services
            .AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                if (passwordOptions is not null)
                {
                    options.Password.RequireDigit = passwordOptions.RequireDigit;
                    options.Password.RequiredLength = passwordOptions.RequiredLength;
                    options.Password.RequireLowercase = passwordOptions.RequireLowercase;
                    options.Password.RequireUppercase = passwordOptions.RequireUppercase;
                    options.Password.RequiredUniqueChars = passwordOptions.RequiredUniqueChars;
                    options.Password.RequireNonAlphanumeric = passwordOptions.RequireNonAlphanumeric;
                    options.Lockout.MaxFailedAccessAttempts = passwordOptions.MaxFailedAttempts;
                }
            })
            .AddEntityFrameworkStores<AuthDbContext>()
            .AddDefaultTokenProviders();
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
                        var userId = context.Principal?.Claims.GetUserId();
                        if (string.IsNullOrWhiteSpace(userId))
                            context.Fail("Unauthorized");

                        return Task.CompletedTask;
                    }
                };
            });
        builder.Services.AddAuthorization(options =>
        {
            options.FallbackPolicy = options.DefaultPolicy;
            options.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser()
                .Build();
        });
    }

    internal static void ApplyMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AuthDbContext>();
        if (db.Database.GetPendingMigrations().Any())
        {
            db.Database.Migrate();
        }
    }
}