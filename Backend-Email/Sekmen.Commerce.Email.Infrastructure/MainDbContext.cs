namespace Sekmen.Commerce.Email.Infrastructure;

public class MainDbContext(
    DbContextOptions options
) : DbContext(options);