namespace Sekmen.Commerce.Services.Carts.Domain.Products;

public record ProductDto(
    int Id,
    string Name,
    double Price,
    string Description,
    string CategoryName,
    string ImageUrl
);