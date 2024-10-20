namespace Sekmen.Commerce.Services.Carts.Application.Carts;

public record CreateOrUpdateCartCommand(ShoppingCartDto ShoppingCart) : ICommand<Result<ShoppingCartDto>>;

internal sealed class CartCommandHandler(
    CartDbContext context
) : ICommandHandler<CreateOrUpdateCartCommand, Result<ShoppingCartDto>>
{
    public async Task<Result<ShoppingCartDto>> Handle(CreateOrUpdateCartCommand request, CancellationToken cancellationToken)
    {
        var cart = await context.Carts.FirstOrDefaultAsync(m => m.UserId == request.ShoppingCart.Cart.UserId, cancellationToken);
        if (cart is null)
        {
            //create cart
            var result = await context.AddAsync(new Cart(request.ShoppingCart.Cart.UserId, request.ShoppingCart.Cart.CouponCode), cancellationToken);
            cart = result.Entity;
        }
        else
        {
            //delete details
            context.CartDetails.RemoveRange(context.CartDetails.Where(m => m.CartId == request.ShoppingCart.Cart.Id));
            cart.Update(request.ShoppingCart.Cart.CouponCode);
        }

        //add details
        await context.AddRangeAsync(request.ShoppingCart.Items.Select(m => new CartDetail(cart, m.Id, m.Count)), cancellationToken);
        _ = await context.SaveChangesAsync(cancellationToken);

        return Result.Ok(request.ShoppingCart);
    }
}