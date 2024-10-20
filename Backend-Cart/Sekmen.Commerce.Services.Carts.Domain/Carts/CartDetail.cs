namespace Sekmen.Commerce.Services.Carts.Domain.Carts;

[Index(nameof(CartId))]
public class CartDetail
{
    [Key] public int Id { get; protected init; }
    [Required] public int CartId { get; protected init; }
    [Required] public Cart Cart { get; protected init; }
    [Required] public int ProductId { get; protected init; }
    [Required] public int Count { get; protected set; }

    protected CartDetail()
    {
    }

    public CartDetail(Cart cart, int productId, int count) : this()
    {
        Cart = cart ?? throw new ArgumentNullException(nameof(cart));
        CartId = cart.Id;
        ProductId = productId;
        Count = count;
    }

    public void Update(int count)
    {
        Count = count;
    }
}