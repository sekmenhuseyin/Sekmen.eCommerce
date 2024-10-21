namespace Sekmen.Commerce.Services.Carts.Application.Carts;

public record ApplyCouponCommand(string UserId, string CouponCode) : ICommand<Result<bool>>;

internal sealed class CouponCommandHandler(
    CartDbContext context
) : ICommandHandler<ApplyCouponCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(ApplyCouponCommand request, CancellationToken cancellationToken)
    {
        var cart = await context.Carts.FirstOrDefaultAsync(m => m.UserId == request.UserId, cancellationToken)
                   ?? new Cart(request.UserId);
        cart.Update(request.CouponCode);
        context.Update(cart);

        await context.SaveChangesAsync(cancellationToken);

        return Result.Ok(true);
    }
}