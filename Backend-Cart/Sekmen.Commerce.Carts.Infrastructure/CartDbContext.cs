namespace Sekmen.Commerce.Carts.Infrastructure;

public class CartDbContext(
    DbContextOptions options
) : DbContext(options)
{
    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartDetail> CartDetails { get; set; }
}