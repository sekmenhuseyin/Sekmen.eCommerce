// ReSharper disable NotAccessedPositionalProperty.Global
namespace Sekmen.Commerce.Services.Carts.Application.Carts;

public record GetCartQuery(string UserId) : IQuery<Result<CartViewModel>>;

public record CartViewModel(
    CartDto Cart = default!,
    IEnumerable<CartDetailDto> Items = default!
);

internal sealed class GetCartQueryHandler(
    CartDbContext context,
    IMapper mapper,
    IProductService productService,
    ICouponService couponService
) : IQueryHandler<GetCartQuery, Result<CartViewModel>>
{
    public async Task<Result<CartViewModel>> Handle(GetCartQuery request, CancellationToken cancellationToken)
    {
        var cart = await context.Carts.FirstOrDefaultAsync(m => m.UserId == request.UserId, cancellationToken);
        if (cart is null)
            return Result.Ok(new CartViewModel());

        var details = await context.CartDetails.Where(m => m.CartId == cart.Id).ToArrayAsync(cancellationToken);
        var cartDetailsDto = mapper.Map<IEnumerable<CartDetailDto>>(details).ToArray();
        var cartTotal = cartDetailsDto.Select(m => m.Count * m.Product?.Price ?? 0).Sum();
        var products = await productService.GetProducts(cartDetailsDto.Select(m => m.ProductId));

        if (!string.IsNullOrWhiteSpace(cart.CouponCode))
        {
            var couponDto = await couponService.GetCoupon(cart.CouponCode);
            if (cartTotal >= couponDto.MinAmount)
            {
                var discountAmount = cartTotal * couponDto.DiscountAmount / 100;
                cart.Update(discountAmount, cartTotal);
            }
            
        }

        var cartDto = mapper.Map<CartDto>(cart);
        foreach (var dto in cartDetailsDto)
        {
            dto.Product = products.FirstOrDefault(m => m.Id == dto.ProductId);
        }

        return Result.Ok(new CartViewModel(
            cartDto,
            cartDetailsDto
        ));
    }
}