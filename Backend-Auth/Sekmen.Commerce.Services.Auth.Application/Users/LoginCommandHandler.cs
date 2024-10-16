namespace Sekmen.Commerce.Services.Auth.Application.Users;

public record LoginCommand(string UserName, string Passwords) : ICommand<ResponseDto<LoginResponseViewModel>>;
public record LoginResponseViewModel(UserDto User, string Token);

internal sealed class LoginCommandHandler : ICommandHandler<LoginCommand, ResponseDto<LoginResponseViewModel>>
{
    public Task<ResponseDto<LoginResponseViewModel>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}