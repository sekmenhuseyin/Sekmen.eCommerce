namespace Sekmen.Commerce.Auth.Application.Users;

public record UpdatePasswordCommand(int Id, string? UserId) : ICommand<Result<bool>>;

internal sealed class UpdatePasswordCommandHandler : ICommandHandler<UpdatePasswordCommand, Result<bool>>
{
    public Task<Result<bool>> Handle(UpdatePasswordCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}