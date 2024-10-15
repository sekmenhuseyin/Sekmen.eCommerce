namespace Sekmen.Commerce.Services.Coupons.Api.Extensions;

public static class WebApplicationExtensions
{
    internal static void AddInternalDependencies(this WebApplicationBuilder builder)
    {
        _ = builder.Services
            .AddAutoMapper(typeof(ICommand))
            .AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<ICommand>())
            .AddDbContext<CouponDbContext>(options => 
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
            );
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