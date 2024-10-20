namespace Sekmen.Commerce.Services.Carts.Application.Carts;

public record CreateOrUpdateCartCommand(CartDto CartDto) : ICommand<Result<CartDto>>;

internal sealed class CartCommandHandler(
    CartDbContext context,
    IMapper mapper
) : ICommandHandler<CreateOrUpdateCartCommand, Result<CartDto>>
{
    public Task<Result<CartDto>> Handle(CreateOrUpdateCartCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}