namespace Sekmen.Commerce.Services.CouponInfrastructure;

public class CouponDbContext(
    DbContextOptions options
) : DbContext(options)
{
    public DbSet<Coupon> Coupons { get; set; }
}