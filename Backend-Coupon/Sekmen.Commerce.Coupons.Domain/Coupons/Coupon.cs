namespace Sekmen.Commerce.Coupons.Domain.Coupons;

[Index(nameof(Code))]
public class Coupon
{
    [Key] public int Id { get; protected init; }
    [Required] public string Code { get; protected init; }
    [Required] public double DiscountAmount { get; protected init; }
    [Required] public int MinAmount { get; protected init; }

    protected Coupon()
    {
    }

    public Coupon(string code, double discountAmount, int minAmount) : this()
    {
        Code = code ?? throw new ArgumentNullException(nameof(code));
        DiscountAmount = discountAmount > 0 ? discountAmount : throw new ArgumentOutOfRangeException(nameof(discountAmount));
        MinAmount = minAmount;
    }
}