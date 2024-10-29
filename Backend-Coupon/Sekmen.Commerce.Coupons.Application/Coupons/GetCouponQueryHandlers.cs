// ReSharper disable UnusedType.Global

using Sekmen.Commerce.Coupons.Application.Models;
using Sekmen.Commerce.Coupons.Application.Segregation;

namespace Sekmen.Commerce.Coupons.Application.Coupons;

public record GetAllCouponQuery : IPagedQuery<Result<IPagedQueryResult<IEnumerable<CouponDto>>>>
{
    public int PageIndex { get; init; } = 1;
    public int PageSize { get; init; } = 10;
    public string OrderBy { get; init; } = "code_asc";
    public string Search { get; init; } = string.Empty;
}
public record GetByIdCouponQuery(int Id) : IQuery<Result<CouponDto>>;
public record GetByCodeCouponQuery(string Code) : IQuery<Result<CouponDto>>;

internal sealed class GetCouponQueryHandler(
    CouponDbContext context,
    IMapper mapper
) : IQueryHandler<GetAllCouponQuery, Result<IPagedQueryResult<IEnumerable<CouponDto>>>>, 
    IQueryHandler<GetByIdCouponQuery, Result<CouponDto>>, 
    IQueryHandler<GetByCodeCouponQuery, Result<CouponDto>>
{
    public async Task<Result<IPagedQueryResult<IEnumerable<CouponDto>>>> Handle(GetAllCouponQuery request, CancellationToken cancellationToken)
    {
        var query = await context.Coupons
            .Filter(x => x.Code.Contains(request.Search), request.Search)
            .Sort(x => x.Code, request.OrderBy)
            .Sort(x => x.Id, request.OrderBy)
            .AsNoTracking()
            .ToArrayAsync(cancellationToken: cancellationToken);

        var result = query
            .GetPaged(request)
            .AsEnumerable()
            .Select(mapper.Map<CouponDto>)
            .ToArray();

        return Result.Ok(result.ToPagedQueryResult(request, query.Length));
    }

    public async Task<Result<CouponDto>> Handle(GetByIdCouponQuery request, CancellationToken cancellationToken)
    {
        var model = await context.Coupons
            .FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken);

        return model == null
            ? Result.Fail<CouponDto>("Coupon not found")
            : Result.Ok(mapper.Map<CouponDto>(model));
    }

    public async Task<Result<CouponDto>> Handle(GetByCodeCouponQuery request, CancellationToken cancellationToken)
    {
        var model = await context.Coupons
            .FirstOrDefaultAsync(m => m.Code.ToUpper() == request.Code.ToUpper(), cancellationToken);

        return model == null
            ? Result.Fail<CouponDto>("Coupon not found")
            : Result.Ok(mapper.Map<CouponDto>(model));
    }
}