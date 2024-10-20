namespace Sekmen.Commerce.Services.Carts.Api.Controllers;

[Route("api/carts")]
[ApiController]
public class CartsController(
    ISender mediatr
) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetItems(GetCartQuery request, CancellationToken cancellationToken)
    {
        var result = await mediatr.Send(request, cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrUpdate([FromBody] CreateOrUpdateCartCommand request, CancellationToken cancellationToken)
    {
        var result = await mediatr.Send(request, cancellationToken);
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteCartCommand request, CancellationToken cancellationToken)
    {
        var result = await mediatr.Send(request, cancellationToken);
        return Ok(result);
    }
}