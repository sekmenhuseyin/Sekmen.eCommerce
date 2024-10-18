namespace Sekmen.Commerce.Services.Auth.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
// [Authorize]
public class UsersController(
    IMediator mediator
) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        return Ok(await mediator.Send(new GetUsersQuery(), cancellationToken));
    }
    
    [HttpDelete]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Delete([FromBody] DeleteUserCommand request)
    {
        request = request with
        {
            UserId = HttpContext.GetUserId()
        };
    
        return Ok(await mediator.Send(request));
    }
    
    // [HttpGet("roles")]
    // public IActionResult GetRoles()
    // {
    //     return Ok(roleQuery.GetRoles());
    // }
    
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateUserCommand request)
    {
        request = request with
        {
            UserId = HttpContext.GetUserId()
        };
    
        return Ok(await mediator.Send(request));
    }
    
    [HttpPut("password")]
    public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordCommand request)
    {
        request = request with
        {
            UserId = HttpContext.GetUserId()
        };
    
        return Ok(await mediator.Send(request));
    }
}