namespace Sekmen.Commerce.Services.CouponApplication.Coupons;

public record GetCouponQuery : IQuery<CouponDto[]>;

internal sealed class GetCouponQueryHandler(
    CouponDbContext context,
    IMapper mapper
) : IQueryHandler<GetCouponQuery, CouponDto[]>
{
    public Task<CouponDto[]> Handle(GetCouponQuery request, CancellationToken cancellationToken)
    {
        var model = context.Coupons
            .AsEnumerable()
            .Select(mapper.Map<CouponDto>)
            .ToArray();

        return Task.FromResult(model);
    }
}