namespace Sekmen.Commerce.Frontend.Application.Models.Carts;

public record CartDetailDto(
    int ProductId,
    int Count
)
{
    public int Id { get; set; }
    public int CartId { get; set; }
    public ProductDto? Product { get; set; }
}