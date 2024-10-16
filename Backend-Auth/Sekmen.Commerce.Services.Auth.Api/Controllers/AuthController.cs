namespace Sekmen.Commerce.Services.Auth.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(
    ISender mediatr
) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterCommand command, CancellationToken cancellationToken)
    {
        var result = await mediatr.Send(command, cancellationToken);
        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginCommand command, CancellationToken cancellationToken)
    {
        var result = await mediatr.Send(command, cancellationToken);
        return Ok(result);
    }

    [HttpPost("assign-role")]
    public async Task<IActionResult> AssignRole([FromBody] AssignRoleCommand command, CancellationToken cancellationToken)
    {
        var result = await mediatr.Send(command, cancellationToken);
        return Ok(result);
    }
}