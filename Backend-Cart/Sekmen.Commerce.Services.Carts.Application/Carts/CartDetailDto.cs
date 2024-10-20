namespace Sekmen.Commerce.Services.Carts.Application.Carts;

public record CartDetailDto(
    int Id,
    int CartId,
    int ProductId,
    int Count
)
{
    public ProductDto? Product { get; set; }
    public CartDto? Cart { get; set; }
}