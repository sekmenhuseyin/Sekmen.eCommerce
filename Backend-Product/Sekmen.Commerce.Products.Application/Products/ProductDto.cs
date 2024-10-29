namespace Sekmen.Commerce.Products.Application.Products;

public record ProductDto(
    int Id,
    string Name,
    double Price,
    string Description,
    string CategoryName,
    string ImageUrl
);