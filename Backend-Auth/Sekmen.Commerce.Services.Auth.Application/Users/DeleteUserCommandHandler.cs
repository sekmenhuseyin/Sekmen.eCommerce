namespace Sekmen.Commerce.Services.Auth.Application.Users;

public record DeleteUserCommand(int Id, string? UserId) : ICommand<Result<bool>>;

internal sealed class DeleteUserCommandHandler : ICommandHandler<DeleteUserCommand, Result<bool>>
{
    public Task<Result<bool>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}