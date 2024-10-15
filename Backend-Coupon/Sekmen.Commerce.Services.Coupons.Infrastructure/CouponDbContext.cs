namespace Sekmen.Commerce.Services.Coupons.Infrastructure;

public class CouponDbContext(
    DbContextOptions options
) : DbContext(options)
{
    public DbSet<Coupon> Coupons { get; set; }
}