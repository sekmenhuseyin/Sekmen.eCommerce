using Sekmen.Commerce.Carts.Application.Models;
using Sekmen.Commerce.Carts.Application.Segregation;

namespace Sekmen.Commerce.Carts.Application.Carts;

public record SendEmailCartCommand(string UserId, int CartId) : ICommand<Result<bool>>;

internal sealed class SendEmailCartCommandHandler(
) : ICommandHandler<SendEmailCartCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(SendEmailCartCommand request, CancellationToken cancellationToken)
    {
        return Result.Ok(true);
    }
}