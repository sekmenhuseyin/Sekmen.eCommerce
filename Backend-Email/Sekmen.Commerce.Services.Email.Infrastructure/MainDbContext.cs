namespace Sekmen.Commerce.Services.Email.Infrastructure;

public class MainDbContext(
    DbContextOptions options
) : DbContext(options);