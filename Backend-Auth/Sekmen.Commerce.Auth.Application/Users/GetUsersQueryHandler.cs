namespace Sekmen.Commerce.Auth.Application.Users;

public record GetUsersQuery : IPagedQuery<IPagedQueryResult<IEnumerable<UserDto>>>
{
    public int PageIndex { get; init; } = 1;
    public int PageSize { get; init; } = 10;
    public string OrderBy { get; init; } = "name_asc";
    public string Search { get; init; } = string.Empty;
}

internal sealed class GetUsersQueryHandler(
    AuthDbContext context,
    IMapper mapper
) : IPagedQueryHandler<GetUsersQuery, IPagedQueryResult<IEnumerable<UserDto>>>
{
    public async Task<IPagedQueryResult<IEnumerable<UserDto>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var query = await context.ApplicationUsers
            .Filter(x => x.Name.Contains(request.Search), request.Search)
            .Sort(x => x.Name, request.OrderBy)
            .Sort(x => x.Id, request.OrderBy)
            .AsNoTracking()
            .ToArrayAsync(cancellationToken: cancellationToken);

        var result = query
            .GetPaged(request)
            .AsEnumerable()
            .Select(mapper.Map<UserDto>)
            .ToArray();

        return result.ToPagedQueryResult(request, query.Length);
    }
}