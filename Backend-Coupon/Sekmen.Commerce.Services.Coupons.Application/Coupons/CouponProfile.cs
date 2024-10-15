// ReSharper disable UnusedType.Global
namespace Sekmen.Commerce.Services.Coupons.Application.Coupons;

public class CouponProfile : Profile
{
    public CouponProfile()
    {
        _ = CreateMap<Coupon, CouponDto>().ReverseMap();
    }
}