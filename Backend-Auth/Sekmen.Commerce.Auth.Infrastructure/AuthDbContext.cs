// ReSharper disable PropertyCanBeMadeInitOnly.Global
namespace Sekmen.Commerce.Auth.Infrastructure;

public class AuthDbContext(
    DbContextOptions options,
    IServiceScopeFactory scopeFactory
) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var saveStatus = await base.SaveChangesAsync(cancellationToken);
        await this.DispatchDomainEventsAsync(scopeFactory);
        return saveStatus;
    }
}