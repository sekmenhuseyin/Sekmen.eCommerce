namespace Sekmen.Commerce.Services.Auth.Application.Users;

public record GetRolesQuery : IQuery<Result<List<string>>>;

internal sealed class GetRolesQueryHandler : IQueryHandler<GetRolesQuery, Result<List<string>>>
{
    public Task<Result<List<string>>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
    {
        var model = new List<string>
        {
            AuthConstants.RoleAdmin,
            AuthConstants.RoleCustomer
        };

        return Task.FromResult(Result.Ok(model));
    }
}