// ReSharper disable NotAccessedPositionalProperty.Global

using Sekmen.Commerce.Carts.Application.Coupons;
using Sekmen.Commerce.Carts.Application.Models;
using Sekmen.Commerce.Carts.Application.Segregation;
using Sekmen.Commerce.Carts.Application.Services;

namespace Sekmen.Commerce.Carts.Application.Carts;

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

        //get cart details
        var details = await context.CartDetails.Where(m => m.CartId == cart.Id).ToArrayAsync(cancellationToken);
        var cartDetailsDto = mapper.Map<IEnumerable<CartDetailDto>>(details).ToArray();
        if (cartDetailsDto.Length == 0)
            return Result.Ok(new CartViewModel(
                mapper.Map<CartDto>(cart),
                cartDetailsDto
            ));

        //update product details
        var products = await productService.GetProducts(cartDetailsDto.Select(m => m.ProductId));
        foreach (var dto in cartDetailsDto)
        {
            dto.Product = products.FirstOrDefault(m => m.Id == dto.ProductId);
        }
        var cartTotal = cartDetailsDto.Select(m => m.Count * m.Product?.Price ?? 0).Sum();

        //update coupon details
        CouponDto? couponDto = default;
        if (string.IsNullOrWhiteSpace(cart.CouponCode))
        {
            cart.Update(0, cartTotal);
        }
        else
        {
            couponDto = await couponService.GetCoupon(cart.CouponCode);
            if (couponDto is not null && cartTotal >= couponDto.MinAmount)
            {
                var discountAmount = cartTotal * couponDto.DiscountAmount / 100;
                cart.Update(discountAmount, cartTotal - discountAmount);
            }
        }

        var cartDto = mapper.Map<CartDto>(cart);
        if (couponDto is not null) cartDto.Coupon = couponDto;

        return Result.Ok(new CartViewModel(
            cartDto,
            cartDetailsDto
        ));
    }
}