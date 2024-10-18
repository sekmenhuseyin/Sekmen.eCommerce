namespace Sekmen.Commerce.Services.Auth.Application.Users;

public record UpdateUserCommand(int Id, int UserId) : ICommand<bool>;

internal sealed class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand, bool>
{
    public Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}