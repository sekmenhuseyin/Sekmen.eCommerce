namespace Sekmen.Commerce.Services.Carts.Application.Carts;

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