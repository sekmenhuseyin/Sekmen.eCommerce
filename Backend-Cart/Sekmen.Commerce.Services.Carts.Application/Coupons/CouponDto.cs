namespace Sekmen.Commerce.Services.Carts.Application.Coupons;

public record CouponDto(
    int Id,
    string Code,
    double DiscountAmount,
    int MinAmount
);