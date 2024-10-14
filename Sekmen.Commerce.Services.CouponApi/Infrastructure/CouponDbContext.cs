namespace Sekmen.Commerce.Services.CouponApi.Infrastructure;

public class CouponDbContext(
    DbContextOptions options
) : DbContext(options)
{
    public DbSet<Coupon> Coupons { get; set; }
}