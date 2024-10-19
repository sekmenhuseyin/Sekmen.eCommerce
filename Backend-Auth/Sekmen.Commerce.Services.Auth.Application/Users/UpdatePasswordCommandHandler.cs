namespace Sekmen.Commerce.Services.Auth.Application.Users;

public record UpdatePasswordCommand(int Id, string? UserId) : ICommand<bool>;

internal sealed class UpdatePasswordCommandHandler : ICommandHandler<UpdatePasswordCommand, bool>
{
    public Task<bool> Handle(UpdatePasswordCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}