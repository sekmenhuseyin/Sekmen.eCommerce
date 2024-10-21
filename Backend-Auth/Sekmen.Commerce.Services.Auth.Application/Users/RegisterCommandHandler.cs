namespace Sekmen.Commerce.Services.Auth.Application.Users;

public record RegisterCommand(string Email, string Name, string PhoneNumber, string Password, string Role) : ICommand<Result<bool>>;

internal sealed class RegisterCommandHandler(
    UserManager<ApplicationUser> userManager,
    RoleManager<IdentityRole> roleManager
) : ICommandHandler<RegisterCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var user = new ApplicationUser
        {
            UserName = request.Email,
            NormalizedUserName = request.Email.ToUpper(),
            Email = request.Email,
            NormalizedEmail = request.Email.ToUpper(),
            Name = request.Name,
            PhoneNumber = request.PhoneNumber
        };

        var result = await userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
            return Result.Fail<bool>(result.Errors.First().Description);

        var roleName = request.Role.ToUpperInvariant();
        var isExists = await roleManager.RoleExistsAsync(roleName);
        if (!isExists) await roleManager.CreateAsync(new IdentityRole(roleName));
        await userManager.AddToRoleAsync(user, roleName);

        return Result.Ok(true);
    }
}