namespace Sekmen.Commerce.Frontend.Application.Models.Carts;

public record CartDetailDto(
    int Id,
    int CartId,
    int ProductId,
    int Count
)
{
    public ProductDto? Product { get; set; }
}