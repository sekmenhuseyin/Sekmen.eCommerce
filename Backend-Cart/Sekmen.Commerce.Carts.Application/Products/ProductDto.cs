namespace Sekmen.Commerce.Carts.Application.Products;

public record ProductDto(
    int Id,
    string Name,
    double Price,
    string Description,
    string CategoryName,
    string ImageUrl
);