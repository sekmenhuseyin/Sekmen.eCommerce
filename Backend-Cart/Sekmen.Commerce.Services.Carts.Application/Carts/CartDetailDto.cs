namespace Sekmen.Commerce.Services.Carts.Application.Carts;

public record CartDetailDto(
    int Id,
    int CartId,
    CartDto Cart,
    int ProductId,
    int Count
)
{
    public ProductDto? Product { get; set; }
}