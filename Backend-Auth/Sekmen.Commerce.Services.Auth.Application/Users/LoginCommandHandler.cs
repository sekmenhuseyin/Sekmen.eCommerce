namespace Sekmen.Commerce.Services.Auth.Application.Users;

public record LoginCommand(string UserName, string Password) : ICommand<ResponseDto<LoginResponseViewModel>>;
public record LoginResponseViewModel(UserDto User, string Token);

internal sealed class LoginCommandHandler(
    AuthDbContext context,
    UserManager<ApplicationUser> userManager,
    RoleManager<IdentityRole> roleManager,
    IMapper mapper
) : ICommandHandler<LoginCommand, ResponseDto<LoginResponseViewModel>>
{
    public async Task<ResponseDto<LoginResponseViewModel>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await context.ApplicationUsers.FirstOrDefaultAsync(m => m.NormalizedUserName == request.UserName.ToUpper(), cancellationToken);
        if (user is null)
        {
            return ResponseDto<LoginResponseViewModel>.NotFound();
        }

        var isValid = await userManager.CheckPasswordAsync(user, request.Password);
        if (!isValid)
        {
            return ResponseDto<LoginResponseViewModel>.NotFound();
        }

        var userDto = mapper.Map<UserDto>(user);
        var model = new LoginResponseViewModel(userDto, "");
        return ResponseDto<LoginResponseViewModel>.Success(model);
    }
}