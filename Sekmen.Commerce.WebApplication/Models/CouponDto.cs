namespace Sekmen.Commerce.WebApplication.Models;

public record CouponDto(
    int Id,
    string Code,
    double DiscountAmount,
    int MinAmount
);