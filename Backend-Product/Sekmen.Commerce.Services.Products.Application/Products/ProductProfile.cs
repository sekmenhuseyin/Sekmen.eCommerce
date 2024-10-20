namespace Sekmen.Commerce.Services.Products.Application.Products;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        _ = CreateMap<Product, ProductDto>().ReverseMap();
    }
}