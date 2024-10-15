using Microsoft.AspNetCore.Identity;

namespace Sekmen.Commerce.Services.Auth.Api.Extensions;

public static class WebApplicationExtensions
{
    internal static void AddInternalDependencies(this WebApplicationBuilder builder)
    {
        _ = builder.Services
            //.AddAutoMapper(typeof(ICommand))
            //.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<ICommand>())
            .AddDbContext<AuthDbContext>(options => 
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
            )
            .AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<AuthDbContext>()
            .AddDefaultTokenProviders();
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