// ReSharper disable PropertyCanBeMadeInitOnly.Global
namespace Sekmen.Commerce.Services.Auth.Infrastructure;

public class AuthDbContext(
    DbContextOptions options
) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
}