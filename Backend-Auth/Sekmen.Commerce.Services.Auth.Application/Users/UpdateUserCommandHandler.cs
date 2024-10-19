namespace Sekmen.Commerce.Services.Auth.Application.Users;

public record UpdateUserCommand(int Id, string? UserId) : ICommand<Result<bool>>;

internal sealed class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand, Result<bool>>
{
    public Task<Result<bool>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}