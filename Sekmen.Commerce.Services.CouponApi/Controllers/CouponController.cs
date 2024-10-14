namespace Sekmen.Commerce.Services.CouponApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CouponController(
    ISender mediatr
) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await mediatr.Send(new GetCouponQuery()));
    }
}