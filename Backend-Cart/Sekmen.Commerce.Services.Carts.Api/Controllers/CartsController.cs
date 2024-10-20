namespace Sekmen.Commerce.Services.Carts.Api.Controllers;

[Route("api/carts")]
[ApiController]
public class CartsController(
    ISender mediatr
) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateOrUpdate([FromBody] CreateOrUpdateCartCommand request, CancellationToken cancellationToken)
    {
        var result = await mediatr.Send(request, cancellationToken);
        return Ok(result);
    }
}