namespace Sekmen.Commerce.Frontend.Application.Models.Products;

public record ProductDto(
    int Id,
    string Name,
    double Price,
    string Description,
    string CategoryName,
    string ImageUrl
);