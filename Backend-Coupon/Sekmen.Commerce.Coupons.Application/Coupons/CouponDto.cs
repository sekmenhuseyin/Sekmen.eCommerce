namespace Sekmen.Commerce.Coupons.Application.Coupons;

public record CouponDto(
    int Id,
    string Code,
    double DiscountAmount,
    int MinAmount
);