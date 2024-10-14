namespace Sekmen.Commerce.Web.Models;

public record CouponDto(
    int Id,
    string Code,
    double DiscountAmount,
    int MinAmount
);