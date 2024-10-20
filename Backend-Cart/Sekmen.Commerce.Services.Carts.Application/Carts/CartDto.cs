namespace Sekmen.Commerce.Services.Carts.Application.Carts;

public record CartDto(
    int Id,
    string UserId,
    string CouponCode,
    CouponDto Coupon,
    double DiscountAmount,
    double Total
);