// ReSharper disable UnusedType.Global
namespace Sekmen.Commerce.Services.Products.Application.Products;

public record GetAllProductQuery : IPagedQuery<IPagedQueryResult<IEnumerable<ProductDto>>>
{
    public int PageIndex { get; init; } = 1;
    public int PageSize { get; init; } = 10;
    public string OrderBy { get; init; } = "name_asc";
    public string Search { get; init; } = string.Empty;
}
public record GetByIdProductQuery(int Id) : IQuery<Result<ProductDto>>;

internal sealed class GetProductQueryHandler(
    ProductDbContext context,
    IMapper mapper
) : IQueryHandler<GetAllProductQuery, IPagedQueryResult<IEnumerable<ProductDto>>>, 
    IQueryHandler<GetByIdProductQuery, Result<ProductDto>>
{
    public async Task<IPagedQueryResult<IEnumerable<ProductDto>>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
    {
        var query = await context.Products
            .Filter(x => x.Name.Contains(request.Search), request.Search)
            .Sort(x => x.Name, request.OrderBy)
            .Sort(x => x.Id, request.OrderBy)
            .AsNoTracking()
            .ToArrayAsync(cancellationToken: cancellationToken);

        var result = query
            .GetPaged(request)
            .AsEnumerable()
            .Select(mapper.Map<ProductDto>)
            .ToArray();

        return result.ToPagedQueryResult(request, query.Length);
    }

    public async Task<Result<ProductDto>> Handle(GetByIdProductQuery request, CancellationToken cancellationToken)
    {
        var model = await context.Products
            .FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken);

        return model == null
            ? Result.Fail<ProductDto>("Product not found")
            : Result.Ok(mapper.Map<ProductDto>(model));
    }
}