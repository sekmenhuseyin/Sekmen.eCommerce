namespace Sekmen.Commerce.Services.Auth.Application.Users;

public record LoginCommand(string UserName, string Password) : ICommand<Result<LoginResponseViewModel>>;
public record LoginResponseViewModel(UserDto User, string Token);

internal sealed class LoginCommandHandler(
    AuthDbContext context,
    UserManager<ApplicationUser> userManager,
    IMapper mapper,
    IJwtTokenGenerator jwtTokenGenerator
) : ICommandHandler<LoginCommand, Result<LoginResponseViewModel>>
{
    public async Task<Result<LoginResponseViewModel>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await context.ApplicationUsers.FirstOrDefaultAsync(m => m.NormalizedUserName == request.UserName.ToUpperInvariant(), cancellationToken);
        if (user is null)
        {
            return Result.Fail("User not found");
        }

        var isValid = await userManager.CheckPasswordAsync(user, request.Password);
        if (!isValid)
        {
            return Result.Fail("Passwords is wrong");
        }

        var roles = await userManager.GetRolesAsync(user);
        var token = jwtTokenGenerator.GenerateToken(user, roles);

        var userDto = mapper.Map<UserDto>(user);
        var model = new LoginResponseViewModel(userDto, token);

        return Result.Ok(model);
    }
}