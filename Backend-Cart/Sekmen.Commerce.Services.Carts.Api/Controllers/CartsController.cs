namespace Sekmen.Commerce.Services.Carts.Api.Controllers;

[Route("api/carts")]
[ApiController]
public class CartsController(
    ISender mediatr
) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateOrUpdate([FromBody] ShoppingCartDto cartDto, CancellationToken cancellationToken)
    {
        var result = await mediatr.Send(new CreateOrUpdateCartCommand(cartDto), cancellationToken);
        return Ok(result);
    }
}