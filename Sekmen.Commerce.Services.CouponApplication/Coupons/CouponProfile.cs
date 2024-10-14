// ReSharper disable UnusedType.Global
namespace Sekmen.Commerce.Services.CouponApplication.Coupons;

public class CouponProfile : Profile
{
    public CouponProfile()
    {
        _ = CreateMap<Coupon, CouponDto>().ReverseMap();
    }
}