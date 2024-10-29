namespace Sekmen.Commerce.Auth.Application.Users;

public record GetRolesQuery : IQuery<Result<List<string>>>;
public record GetRoleQuery(string Id) : IQuery<Result<IList<string>>>;

internal sealed class GetRolesQueryHandler(
    AuthDbContext context,
    UserManager<ApplicationUser> userManager
) : IQueryHandler<GetRolesQuery, Result<List<string>>>,
    IQueryHandler<GetRoleQuery, Result<IList<string>>>
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

    public async Task<Result<IList<string>>> Handle(GetRoleQuery request, CancellationToken cancellationToken)
    {
        var user = await context.ApplicationUsers.FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken);
        if (user is null)
        {
            return Result.Fail<IList<string>>("User not found");
        }

        var roles = await userManager.GetRolesAsync(user);

        return Result.Ok(roles);
    }
}