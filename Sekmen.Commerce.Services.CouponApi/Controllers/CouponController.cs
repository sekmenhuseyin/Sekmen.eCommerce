using Sekmen.Commerce.Services.CouponApi.Models;

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
        var coupons = await mediatr.Send(new GetAllCouponQuery()); 
        return Ok(new ResponseDto().Success(coupons));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var coupon = await mediatr.Send(new GetByIdCouponQuery(id));
        return Ok(coupon == null
         ? new ResponseDto().NotFound()
         : new ResponseDto().Success(coupon));
    }

    [HttpGet("{code}")]
    public async Task<IActionResult> GetById([FromRoute] string code)
    {
        var coupon = await mediatr.Send(new GetByCodeCouponQuery(code));
        return Ok(coupon == null
            ? new ResponseDto().NotFound()
            : new ResponseDto().Success(coupon));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CouponDto couponDto)
    {
        var result = await mediatr.Send(new CreateCouponCommand(couponDto));
        return Ok(result
            ? new ResponseDto().Success(couponDto)
            : new ResponseDto().Error("error"));
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] CouponDto couponDto)
    {
        var result = await mediatr.Send(new UpdateCouponCommand(couponDto));
        return Ok(result
            ? new ResponseDto().Success(couponDto)
            : new ResponseDto().Error("error"));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var result = await mediatr.Send(new DeleteCouponCommand(id));
        return Ok(result
            ? new ResponseDto().Success(true)
            : new ResponseDto().Error("error"));
    }
}