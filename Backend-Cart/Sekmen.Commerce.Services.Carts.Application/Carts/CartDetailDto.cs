namespace Sekmen.Commerce.Services.Carts.Application.Carts;

public record CartDetailDto(
    int Id,
    int CartId,
    CartDto Cart,
    int ProductId,
    ProductDto Product,
    int Count
);