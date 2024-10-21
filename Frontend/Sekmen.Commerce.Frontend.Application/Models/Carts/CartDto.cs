namespace Sekmen.Commerce.Frontend.Application.Models.Carts;

public record CartDto(
    string UserId
)
{
    public int Id { get; set; }
    public string? CouponCode { get; set; }
    public CouponDto? Coupon { get; set; }
    public double DiscountAmount { get; set; }
    public double Total { get; set; }
}