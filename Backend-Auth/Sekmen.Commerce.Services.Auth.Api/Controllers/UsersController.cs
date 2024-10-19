namespace Sekmen.Commerce.Services.Auth.Api.Controllers;

[Route("api/users")]
[ApiController]
[Authorize]
public class UsersController(
    IMediator mediatr
) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetUsersQuery query, CancellationToken cancellationToken)
    {
        return Ok(await mediatr.Send(query, cancellationToken));
    }
    
    [HttpDelete]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Delete([FromBody] DeleteUserCommand request, CancellationToken cancellationToken)
    {
        request = request with
        {
            UserId = HttpContext.GetUserId()
        };
    
        return Ok(await mediatr.Send(request, cancellationToken));
    }
    
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateUserCommand request, CancellationToken cancellationToken)
    {
        return Ok(await mediatr.Send(request, cancellationToken));
    }
    
    [HttpPut("password")]
    public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordCommand request, CancellationToken cancellationToken)
    {
        request = request with
        {
            UserId = HttpContext.GetUserId()
        };
    
        return Ok(await mediatr.Send(request, cancellationToken));
    }
    
    [HttpGet("role/{id}")]
    public async Task<IActionResult> GetRole([FromRoute] string id, CancellationToken cancellationToken)
    {
        var result = await mediatr.Send(new GetRoleQuery(id), cancellationToken);
        return Ok(result);
    }
}