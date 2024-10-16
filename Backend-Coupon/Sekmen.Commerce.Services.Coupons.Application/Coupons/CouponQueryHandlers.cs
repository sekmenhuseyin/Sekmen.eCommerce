// ReSharper disable UnusedType.Global
namespace Sekmen.Commerce.Services.Coupons.Application.Coupons;

public record GetAllCouponQuery : IQuery<ResponseDto<CouponDto[]>>;
public record GetByIdCouponQuery(int Id) : IQuery<ResponseDto<CouponDto?>>;
public record GetByCodeCouponQuery(string Code) : IQuery<ResponseDto<CouponDto?>>;

internal sealed class GetCouponQueryHandler(
    CouponDbContext context,
    IMapper mapper
) : IQueryHandler<GetAllCouponQuery, ResponseDto<CouponDto[]>>, 
    IQueryHandler<GetByIdCouponQuery, ResponseDto<CouponDto?>>, 
    IQueryHandler<GetByCodeCouponQuery, ResponseDto<CouponDto?>>
{
    public Task<ResponseDto<CouponDto[]>> Handle(GetAllCouponQuery request, CancellationToken cancellationToken)
    {
        var coupons = context.Coupons
            .AsEnumerable()
            .Select(mapper.Map<CouponDto>)
            .ToArray();

        return Task.FromResult(ResponseDto<CouponDto[]>.Success(coupons));
    }

    public async Task<ResponseDto<CouponDto?>> Handle(GetByIdCouponQuery request, CancellationToken cancellationToken)
    {
        var model = await context.Coupons
            .FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken);

        var coupon = mapper.Map<CouponDto>(model);
        return coupon == null
            ? ResponseDto<CouponDto?>.NotFound()
            : ResponseDto<CouponDto?>.Success(coupon);
    }

    public async Task<ResponseDto<CouponDto?>> Handle(GetByCodeCouponQuery request, CancellationToken cancellationToken)
    {
        var model = await context.Coupons
            .FirstOrDefaultAsync(m => m.Code.ToUpper() == request.Code.ToUpper(), cancellationToken);

        var coupon = mapper.Map<CouponDto>(model);
        return coupon == null
            ? ResponseDto<CouponDto?>.NotFound()
            : ResponseDto<CouponDto?>.Success(coupon);
    }
}