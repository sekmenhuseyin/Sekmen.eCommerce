using Sekmen.Commerce.Carts.Application.Coupons;

namespace Sekmen.Commerce.Carts.Application.Carts;

public record CartDto(
    int Id,
    string UserId,
    string CouponCode,
    double DiscountAmount,
    double Total
)
{
    public CouponDto? Coupon { get; set; }
}