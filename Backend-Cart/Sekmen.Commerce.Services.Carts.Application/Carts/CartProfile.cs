namespace Sekmen.Commerce.Services.Carts.Application.Carts;

public class CartProfile : Profile
{
    public CartProfile()
    {
        _ = CreateMap<Cart, CartDto>().ReverseMap();
        _ = CreateMap<CartDetail, CartDetailDto>().ReverseMap();
    }
}