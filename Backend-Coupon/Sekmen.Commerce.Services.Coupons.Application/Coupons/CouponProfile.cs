// ReSharper disable UnusedType.Global
namespace Sekmen.Commerce.Services.Coupons.Application.Coupons;

public class CouponProfile : Profile
{
    public CouponProfile()
    {
        CreateMap<Coupon, CouponDto>().ReverseMap();
    }
}