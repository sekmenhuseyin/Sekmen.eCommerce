namespace Sekmen.Commerce.Services.Coupons.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CouponController(
    ISender mediatr
) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var coupons = await mediatr.Send(new GetAllCouponQuery(), cancellationToken); 
        return Ok(coupons);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id, CancellationToken cancellationToken)
    {
        var coupon = await mediatr.Send(new GetByIdCouponQuery(id), cancellationToken);
        return Ok(coupon);
    }

    [HttpGet("{code}")]
    public async Task<IActionResult> GetById([FromRoute] string code, CancellationToken cancellationToken)
    {
        var coupon = await mediatr.Send(new GetByCodeCouponQuery(code), cancellationToken);
        return Ok(coupon);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CouponDto couponDto, CancellationToken cancellationToken)
    {
        var result = await mediatr.Send(new CreateCouponCommand(couponDto), cancellationToken);
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] CouponDto couponDto, CancellationToken cancellationToken)
    {
        var result = await mediatr.Send(new UpdateCouponCommand(couponDto), cancellationToken);
        return Ok(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
    {
        var result = await mediatr.Send(new DeleteCouponCommand(id), cancellationToken);
        return Ok(result);
    }
}