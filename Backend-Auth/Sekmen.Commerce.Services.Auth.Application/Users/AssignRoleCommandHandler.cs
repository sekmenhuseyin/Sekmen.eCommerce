namespace Sekmen.Commerce.Services.Auth.Application.Users;

public record AssignRoleCommand(string Email, string Role) : ICommand<Result<bool>>;

public class AssignRoleCommandHandler(
    AuthDbContext context,
    UserManager<ApplicationUser> userManager,
    RoleManager<IdentityRole> roleManager
) : ICommandHandler<AssignRoleCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(AssignRoleCommand request, CancellationToken cancellationToken)
    {
        var user = await context.ApplicationUsers.FirstOrDefaultAsync(m => m.NormalizedEmail == request.Email.ToUpperInvariant(), cancellationToken);
        if (user is null)
        {
            return Result.Fail<bool>("User not found");
        }

        var roleName = request.Role.ToUpperInvariant();
        var isExists = await roleManager.RoleExistsAsync(roleName);
        if (!isExists)
        {
            _ = await roleManager.CreateAsync(new IdentityRole(roleName));
        }

        _ = await userManager.AddToRoleAsync(user, roleName);
        return Result.Ok(true);
    }
}