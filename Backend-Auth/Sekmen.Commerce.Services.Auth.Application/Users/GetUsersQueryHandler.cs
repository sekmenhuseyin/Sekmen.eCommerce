namespace Sekmen.Commerce.Services.Auth.Application.Users;

public record GetUsersQuery : IQuery<UserDto[]>
{
    public int PageIndex { get; init; } = 1;
    public int PageSize { get; init; } = 10;
    public string OrderBy { get; init; } = "name_asc";
    public string Search { get; init; } = string.Empty;
    public int? UserId { get; init; }
}

internal sealed class GetUsersQueryHandler(
    AuthDbContext context,
    IMapper mapper
) : IQueryHandler<GetUsersQuery, UserDto[]>
{
    public async Task<UserDto[]> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var query = await context.ApplicationUsers
            .AsNoTracking()
            .ToArrayAsync(cancellationToken: cancellationToken);

        var list = query.Select(mapper.Map<UserDto>).ToArray();
        return list;
    }
}