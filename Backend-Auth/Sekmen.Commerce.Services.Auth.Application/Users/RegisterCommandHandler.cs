namespace Sekmen.Commerce.Services.Auth.Application.Users;

public record RegisterCommand(string Email, string Name, string PhoneNumber, string Password) : ICommand<ResponseDto<bool>>;

internal sealed class RegisterCommandHandler(
    UserManager<ApplicationUser> userManager
) : ICommandHandler<RegisterCommand, ResponseDto<bool>>
{
    public async Task<ResponseDto<bool>> Handle(RegisterCommand request, CancellationToken cancellationToken)
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
        if (result.Succeeded)
        {
            return ResponseDto<bool>.Success(true);
        }

        return ResponseDto<bool>.Error(result.Errors.First().Description);
    }
}