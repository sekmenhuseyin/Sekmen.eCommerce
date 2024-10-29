namespace Sekmen.Commerce.Products.Infrastructure;

public class ProductDbContext(
    DbContextOptions options
) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
}