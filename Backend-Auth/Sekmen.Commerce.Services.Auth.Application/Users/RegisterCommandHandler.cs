namespace Sekmen.Commerce.Services.Auth.Application.Users;

public record RegisterCommand(string Email, string Name, string PhoneNumber) : ICommand<ResponseDto<bool>>;

internal sealed class RegisterCommandHandler : ICommandHandler<RegisterCommand, ResponseDto<bool>>
{
    public Task<ResponseDto<bool>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}