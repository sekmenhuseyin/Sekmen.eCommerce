namespace Sekmen.Commerce.Services.CouponApplication.Coupons;

public record CouponDto(
    int Id,
    string Code,
    double DiscountAmount,
    int MinAmount
);