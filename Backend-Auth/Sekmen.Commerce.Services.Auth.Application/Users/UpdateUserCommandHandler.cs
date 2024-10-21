namespace Sekmen.Commerce.Services.Auth.Application.Users;

public record UpdateUserCommand(string Id, string Name, string PhoneNumber, string Role) : ICommand<Result<bool>>;

internal sealed class UpdateUserCommandHandler(
    AuthDbContext context,
    UserManager<ApplicationUser> userManager
) : ICommandHandler<UpdateUserCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await context.ApplicationUsers.FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken);
        if (user is null)
        {
            return Result.Fail<bool>("User not found");
        }

        user.Name = request.Name;
        user.PhoneNumber = request.PhoneNumber;
        await userManager.UpdateAsync(user);

        var roles = await userManager.GetRolesAsync(user);
        if (roles.First() == request.Role)
            return Result.Ok(true);

        await userManager.RemoveFromRolesAsync(user, roles);
        var roleName = request.Role.ToUpperInvariant();
        await userManager.AddToRoleAsync(user, roleName);

        return Result.Ok(true);
    }
}