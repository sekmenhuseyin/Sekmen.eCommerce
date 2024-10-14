namespace Sekmen.Commerce.Services.CouponApi.Models;

public record CouponDto(
    int Id,
    string Code,
    double DiscountAmount,
    int MinAmount
);