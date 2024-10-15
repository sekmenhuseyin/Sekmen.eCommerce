namespace Sekmen.Commerce.Services.Auth.Infrastructure;

public class AuthDbContext(
    DbContextOptions options
) : IdentityDbContext<IdentityUser>(options)
{
}