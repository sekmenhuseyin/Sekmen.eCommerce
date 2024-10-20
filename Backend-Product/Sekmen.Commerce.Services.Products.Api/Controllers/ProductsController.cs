namespace Sekmen.Commerce.Services.Products.Api.Controllers;

[Route("api/products")]
[ApiController]
[Authorize]
public class ProductsController(
    ISender mediatr
) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetAllProductQuery query, CancellationToken cancellationToken)
    {
        var coupons = await mediatr.Send(query, cancellationToken); 
        return Ok(coupons);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id, CancellationToken cancellationToken)
    {
        var coupon = await mediatr.Send(new GetByIdProductQuery(id), cancellationToken);
        return Ok(coupon);
    }

    [HttpPost]
    [Authorize(Roles = AuthConstants.RoleAdmin)]
    public async Task<IActionResult> Create([FromBody] ProductDto couponDto, CancellationToken cancellationToken)
    {
        var result = await mediatr.Send(new CreateProductCommand(couponDto), cancellationToken);
        return Ok(result);
    }

    [HttpPut]
    [Authorize(Roles = AuthConstants.RoleAdmin)]
    public async Task<IActionResult> Update([FromBody] ProductDto couponDto, CancellationToken cancellationToken)
    {
        var result = await mediatr.Send(new UpdateProductCommand(couponDto), cancellationToken);
        return Ok(result);
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = AuthConstants.RoleAdmin)]
    public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
    {
        var result = await mediatr.Send(new DeleteProductCommand(id), cancellationToken);
        return Ok(result);
    }
}