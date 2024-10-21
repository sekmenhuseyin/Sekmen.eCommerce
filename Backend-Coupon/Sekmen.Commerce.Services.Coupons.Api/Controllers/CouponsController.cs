namespace Sekmen.Commerce.Services.Coupons.Api.Controllers;

[Route("api/coupons")]
[ApiController]
[Authorize]
public class CouponsController(
    ISender mediatr
) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetAllCouponQuery query, CancellationToken cancellationToken)
    {
        var coupons = await mediatr.Send(query, cancellationToken); 
        return Ok(coupons);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id, CancellationToken cancellationToken)
    {
        var coupon = await mediatr.Send(new GetByIdCouponQuery(id), cancellationToken);
        return Ok(coupon);
    }

    [HttpGet("{code}")]
    public async Task<IActionResult> GetByCode([FromRoute] string code, CancellationToken cancellationToken)
    {
        var coupon = await mediatr.Send(new GetByCodeCouponQuery(code), cancellationToken);
        return Ok(coupon);
    }

    [HttpPost]
    [Authorize(Roles = AuthConstants.RoleAdmin)]
    public async Task<IActionResult> Create([FromBody] CouponDto couponDto, CancellationToken cancellationToken)
    {
        var result = await mediatr.Send(new CreateCouponCommand(couponDto), cancellationToken);
        return Ok(result);
    }

    [HttpPut]
    [Authorize(Roles = AuthConstants.RoleAdmin)]
    public async Task<IActionResult> Update([FromBody] CouponDto couponDto, CancellationToken cancellationToken)
    {
        var result = await mediatr.Send(new UpdateCouponCommand(couponDto), cancellationToken);
        return Ok(result);
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = AuthConstants.RoleAdmin)]
    public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
    {
        var result = await mediatr.Send(new DeleteCouponCommand(id), cancellationToken);
        return Ok(result);
    }
}