namespace Sekmen.Commerce.Services.Auth.Application.Users;

public record RegisterCommand(string Email, string Name, string PhoneNumber, string Password) : ICommand<Result<bool>>;

internal sealed class RegisterCommandHandler(
    UserManager<ApplicationUser> userManager
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
        return result.Succeeded
            ? Result.Ok(true)
            : Result.Fail<bool>(result.Errors.First().Description);
    }
}