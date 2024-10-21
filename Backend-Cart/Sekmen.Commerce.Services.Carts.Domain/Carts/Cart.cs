namespace Sekmen.Commerce.Services.Carts.Domain.Carts;

[Index(nameof(UserId))]
public class Cart
{
    [Key] public int Id { get; protected init; }
    [Required] public string UserId { get; protected init; }
    public string? CouponCode { get; protected set; }
    [NotMapped] public double DiscountAmount { get; protected set; }
    [NotMapped] public double Total { get; protected set; }

    protected Cart()
    {
    }

    public Cart(string userId) : this()
    {
        UserId = userId ?? throw new ArgumentNullException(nameof(userId));
    }

    public void Update(string? couponCode)
    {
        CouponCode = couponCode;
    }

    public void Update(double discountAmount, double total)
    {
        DiscountAmount = discountAmount;
        Total = total;
    }
}