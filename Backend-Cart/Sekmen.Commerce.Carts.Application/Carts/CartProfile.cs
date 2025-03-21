namespace Sekmen.Commerce.Carts.Application.Carts;

public class CartProfile : Profile
{
    public CartProfile()
    {
        CreateMap<Cart, CartDto>().ReverseMap();
        CreateMap<CartDetail, CartDetailDto>().ReverseMap();
    }
}