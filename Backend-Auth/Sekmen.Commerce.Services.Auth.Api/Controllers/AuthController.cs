namespace Sekmen.Commerce.Services.Auth.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(
    ISender mediatr,
    AppSettingsModel appSettings
) : ControllerBase
{
    [HttpPost("register")]
    [Consumes("application/x-www-form-urlencoded")]
    public async Task<IActionResult> Register([FromForm] RegisterCommand command, CancellationToken cancellationToken)
    {
        var result = await mediatr.Send(command, cancellationToken);
        return Ok(result);
    }

    [HttpPost("login")]
    [Consumes("application/x-www-form-urlencoded")]
    public async Task<IActionResult> Login([FromForm] LoginCommand command, CancellationToken cancellationToken)
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

    [HttpGet("password-policy")]
    public IActionResult GetPasswordPolicy()
    {
        return Ok(appSettings.PasswordOptions);
    }
}