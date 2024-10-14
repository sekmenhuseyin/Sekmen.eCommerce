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
        var coupon = await mediatr.Send(new GetByIdCouponQuery(id));
        return coupon == null
            ? NotFound()
            : Ok(coupon);
    }

    [HttpGet("{code}")]
    public async Task<IActionResult> GetById([FromRoute] string code)
    {
        var coupon = await mediatr.Send(new GetByCodeCouponQuery(code));
        return coupon == null
            ? NotFound()
            : Ok(coupon);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CouponDto couponDto)
    {
        return Ok(await mediatr.Send(new CreateCouponCommand(couponDto)));
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] CouponDto couponDto)
    {
        return Ok(await mediatr.Send(new UpdateCouponCommand(couponDto)));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        return Ok(await mediatr.Send(new DeleteCouponCommand(id)));
    }
}