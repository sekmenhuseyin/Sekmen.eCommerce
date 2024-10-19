namespace Sekmen.Commerce.Services.Auth.Application.Users;

public record DeleteUserCommand(int Id, string? UserId) : ICommand<bool>;

internal sealed class DeleteUserCommandHandler : ICommandHandler<DeleteUserCommand, bool>
{
    public Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}