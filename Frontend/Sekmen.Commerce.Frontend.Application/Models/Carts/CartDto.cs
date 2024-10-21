namespace Sekmen.Commerce.Frontend.Application.Models.Carts;

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