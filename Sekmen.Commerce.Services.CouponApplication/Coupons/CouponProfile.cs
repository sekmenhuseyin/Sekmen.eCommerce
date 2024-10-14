namespace Sekmen.Commerce.Services.CouponApplication.Coupons;

public class CouponProfile : Profile
{
    public CouponProfile()
    {
        CreateMap<Coupon, CouponDto>();
    }
}