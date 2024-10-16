// ReSharper disable UnusedType.Global
namespace Sekmen.Commerce.Services.Coupons.Application.Coupons;

public record GetAllCouponQuery : IQuery<Result<CouponDto[]>>;
public record GetByIdCouponQuery(int Id) : IQuery<Result<CouponDto>>;
public record GetByCodeCouponQuery(string Code) : IQuery<Result<CouponDto>>;

internal sealed class GetCouponQueryHandler(
    CouponDbContext context,
    IMapper mapper
) : IQueryHandler<GetAllCouponQuery, Result<CouponDto[]>>, 
    IQueryHandler<GetByIdCouponQuery, Result<CouponDto>>, 
    IQueryHandler<GetByCodeCouponQuery, Result<CouponDto>>
{
    public Task<Result<CouponDto[]>> Handle(GetAllCouponQuery request, CancellationToken cancellationToken)
    {
        var coupons = context.Coupons
            .AsEnumerable()
            .Select(mapper.Map<CouponDto>)
            .ToArray();

        return Task.FromResult(Result.Ok(coupons));
    }

    public async Task<Result<CouponDto>> Handle(GetByIdCouponQuery request, CancellationToken cancellationToken)
    {
        var model = await context.Coupons
            .FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken);

        return model == null
            ? Result.Fail("Coupon not found")
            : Result.Ok(mapper.Map<CouponDto>(model));
    }

    public async Task<Result<CouponDto>> Handle(GetByCodeCouponQuery request, CancellationToken cancellationToken)
    {
        var model = await context.Coupons
            .FirstOrDefaultAsync(m => m.Code.ToUpper() == request.Code.ToUpper(), cancellationToken);

        return model == null
            ? Result.Fail("Coupon not found")
            : Result.Ok(mapper.Map<CouponDto>(model));
    }
}