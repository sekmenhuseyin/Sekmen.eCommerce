namespace Sekmen.Commerce.Services.Carts.Application.Carts;

public record ApplyCouponCommand(CartDto Cart) : ICommand<Result<bool>>;

internal sealed class CouponCommandHandler(
    CartDbContext context
) : ICommandHandler<ApplyCouponCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(ApplyCouponCommand request, CancellationToken cancellationToken)
    {
        var cart = await context.Carts.FirstOrDefaultAsync(m => m.UserId == request.Cart.UserId, cancellationToken);
        if (cart is null)
        {
            cart = new Cart(request.Cart.UserId, request.Cart.CouponCode);
        }
        else
        {
            cart.Update(request.Cart.CouponCode);
        }

        context.Update(cart);

        await context.SaveChangesAsync(cancellationToken);

        return Result.Ok(true);
    }
}