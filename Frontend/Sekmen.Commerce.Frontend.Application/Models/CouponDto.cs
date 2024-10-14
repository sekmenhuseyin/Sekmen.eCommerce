namespace Sekmen.Commerce.Frontend.Application.Models;

public record CouponDto(
    int Id,
    string Code,
    double DiscountAmount,
    int MinAmount
);