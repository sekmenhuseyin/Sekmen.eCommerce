namespace Sekmen.Commerce.Services.Carts.Application.Carts;

public record CreateOrUpdateCartCommand(CartDto Cart, CartDetailDto Details) : ICommand<Result<bool>>;

internal sealed class CartCommandHandler(
    CartDbContext context
) : ICommandHandler<CreateOrUpdateCartCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(CreateOrUpdateCartCommand request, CancellationToken cancellationToken)
    {
        var cart = await context.Carts.FirstOrDefaultAsync(m => m.UserId == request.Cart.UserId, cancellationToken);
        if (cart is null)
        {
            //create cart
            var result = await context.AddAsync(new Cart(request.Cart.UserId, request.Cart.CouponCode), cancellationToken);
            cart = result.Entity;
            _ = await context.AddAsync(new CartDetail(cart, request.Details.Id, request.Details.Count), cancellationToken);
        }
        else
        {
            cart.Update(request.Cart.CouponCode);
            context.Update(cart);
            //check if details exists
            var product = await context.CartDetails.FirstOrDefaultAsync(m =>
                m.CartId == cart.Id && m.ProductId == request.Details.ProductId, cancellationToken);
            if (product is null)
            {
                _ = await context.AddAsync(new CartDetail(cart, request.Details.Id, request.Details.Count), cancellationToken);
            }
            else
            {
                product.Update(product.Count + request.Details.Count);
                context.Update(product);
            }
        }

        _ = await context.SaveChangesAsync(cancellationToken);

        return Result.Ok(true);
    }
}