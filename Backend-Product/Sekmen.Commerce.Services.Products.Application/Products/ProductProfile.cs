namespace Sekmen.Commerce.Services.Products.Application.Products;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductDto>().ReverseMap();
    }
}