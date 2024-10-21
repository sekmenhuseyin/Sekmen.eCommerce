namespace Sekmen.Commerce.Services.Carts.Application.Carts;

public record SendEmailCartCommand(string UserId, int CartId) : ICommand<Result<bool>>;

internal sealed class SendEmailCartCommandHandler(
    IMessageBus messageBus
) : ICommandHandler<SendEmailCartCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(SendEmailCartCommand request, CancellationToken cancellationToken)
    {
        await messageBus.PublishAsync(request, "email");
        return Result.Ok(true);
    }
}