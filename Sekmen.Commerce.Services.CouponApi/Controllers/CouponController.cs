namespace Sekmen.Commerce.Services.CouponApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CouponController(
    ISender mediatr
) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await mediatr.Send(new GetAllCouponQuery()));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        return Ok(await mediatr.Send(new GetByIdCouponQuery(id)));
    }

    [HttpGet("{code}")]
    public async Task<IActionResult> GetById([FromRoute] string code)
    {
        return Ok(await mediatr.Send(new GetByCodeCouponQuery(code)));
    }
}