namespace Sekmen.Commerce.Services.Carts.Application.Carts;

public record CreateOrUpdateCartCommand(string UserId, int ProductId, int Count) : ICommand<Result<bool>>;
public record DeleteCartCommand(int DetailsId) : ICommand<Result<bool>>;

internal sealed class CartCommandHandler(
    CartDbContext context
) : ICommandHandler<CreateOrUpdateCartCommand, Result<bool>>,
    ICommandHandler<DeleteCartCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(CreateOrUpdateCartCommand request, CancellationToken cancellationToken)
    {
        var cart = await context.Carts.FirstOrDefaultAsync(m => m.UserId == request.UserId, cancellationToken);
        if (cart is null)
        {
            //create cart
            var result = await context.AddAsync(new Cart(request.UserId), cancellationToken);
            cart = result.Entity;
            await context.AddAsync(new CartDetail(cart, request.ProductId, request.Count), cancellationToken);
        }
        else
        {
            //check if details exists
            var product = await context.CartDetails.FirstOrDefaultAsync(m =>
                m.CartId == cart.Id && m.ProductId == request.ProductId, cancellationToken);
            if (product is null)
            {
                await context.AddAsync(new CartDetail(cart, request.ProductId, request.Count), cancellationToken);
            }
            else
            {
                product.Update(product.Count + request.Count);
                context.Update(product);
            }
        }

        await context.SaveChangesAsync(cancellationToken);

        return Result.Ok(true);
    }

    public Task<Result<bool>> Handle(DeleteCartCommand request, CancellationToken cancellationToken)
    {
        context.Remove(context.CartDetails.Where(m => m.Id == request.DetailsId));

        return Task.FromResult(Result.Ok(true));
    }
}