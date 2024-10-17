namespace Sekmen.Commerce.Frontend.Application.Models.Coupons;

public record CouponDto(
    int Id,
    string Code,
    double DiscountAmount,
    int MinAmount
);