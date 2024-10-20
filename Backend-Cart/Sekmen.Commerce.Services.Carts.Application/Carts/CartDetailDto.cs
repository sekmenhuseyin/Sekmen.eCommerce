namespace Sekmen.Commerce.Services.Carts.Application.Carts;

public record CartDetailDto(
    int Id,
    int CartId,
    int ProductId,
    int Count,
    ProductDto Product
);