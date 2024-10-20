namespace Sekmen.Commerce.Services.Carts.Application.Carts;

public record GetCartQuery(string UserId) : IQuery<Result<CartViewModel>>;

public record CartViewModel(
    CartDto Cart = default!,
    IEnumerable<CartDetailDto> Items = default!
);

internal sealed class GetCartQueryHandler(
    CartDbContext context,
    IMapper mapper
) : IQueryHandler<GetCartQuery, Result<CartViewModel>>
{
    public async Task<Result<CartViewModel>> Handle(GetCartQuery request, CancellationToken cancellationToken)
    {
        var cart = await context.Carts.FirstOrDefaultAsync(m => m.UserId == request.UserId, cancellationToken);
        if (cart is null)
            return Result.Ok(new CartViewModel());

        var details = await context.CartDetails.Where(m => m.CartId == cart.Id).ToArrayAsync(cancellationToken);
        var cartDetailsDto = mapper.Map<IEnumerable<CartDetailDto>>(details).ToArray();
        var cartDto = mapper.Map<CartDto>(cart) with
        {
            Total = cartDetailsDto.Select(m => m.Count * m.Product.Price).Sum()
        };

        return Result.Ok(new CartViewModel(
            cartDto,
            cartDetailsDto ?? []
        ));
    }
}