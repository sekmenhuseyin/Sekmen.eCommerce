using Sekmen.Commerce.Carts.Application.Products;

namespace Sekmen.Commerce.Carts.Application.Carts;

public record CartDetailDto(
    int Id,
    int CartId,
    int ProductId,
    int Count
)
{
    public ProductDto? Product { get; set; }
}