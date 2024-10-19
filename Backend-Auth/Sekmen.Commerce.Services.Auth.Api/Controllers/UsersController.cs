namespace Sekmen.Commerce.Services.Auth.Api.Controllers;

[Route("api/users")]
[ApiController]
[Authorize]
public class UsersController(
    IMediator mediator
) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await mediator.Send(new GetUsersQuery()));
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