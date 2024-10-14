// ReSharper disable UnusedType.Global
namespace Sekmen.Commerce.Services.CouponApplication.Coupons;

public record GetAllCouponQuery : IQuery<CouponDto[]>;
public record GetByIdCouponQuery(int Id) : IQuery<CouponDto>;
public record GetByCodeCouponQuery(string Code) : IQuery<CouponDto>;

internal sealed class GetCouponQueryHandler(
    CouponDbContext context,
    IMapper mapper
) : IQueryHandler<GetAllCouponQuery, CouponDto[]>, 
    IQueryHandler<GetByIdCouponQuery, CouponDto>, 
    IQueryHandler<GetByCodeCouponQuery, CouponDto>
{
    public Task<CouponDto[]> Handle(GetAllCouponQuery request, CancellationToken cancellationToken)
    {
        var model = context.Coupons
            .AsEnumerable()
            .Select(mapper.Map<CouponDto>)
            .ToArray();

        return Task.FromResult(model);
    }

    public async Task<CouponDto> Handle(GetByIdCouponQuery request, CancellationToken cancellationToken)
    {
        var model = await context.Coupons
            .FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken);

        return mapper.Map<CouponDto>(model);
    }

    public async Task<CouponDto> Handle(GetByCodeCouponQuery request, CancellationToken cancellationToken)
    {
        var model = await context.Coupons
            .FirstOrDefaultAsync(m => m.Code.ToUpper() == request.Code.ToUpper(), cancellationToken);

        return mapper.Map<CouponDto>(model);
    }
}