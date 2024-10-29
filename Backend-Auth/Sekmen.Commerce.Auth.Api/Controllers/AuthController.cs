namespace Sekmen.Commerce.Auth.Api.Controllers;

[Route("api/auth")]
[ApiController]
[AllowAnonymous]
public class AuthController(
    ISender mediatr,
    AppSettingsModel appSettings
) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginCommand command, CancellationToken cancellationToken)
    {
        var result = await mediatr.Send(command, cancellationToken);
        return Ok(result);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterCommand command, CancellationToken cancellationToken)
    {
        var result = await mediatr.Send(command, cancellationToken);
        return Ok(result);
    }
    
    [HttpGet("password-policy")]
    public IActionResult GetPasswordPolicy()
    {
        return Ok(appSettings.PasswordOptions);
    }
    
    [HttpGet("roles")]
    public async Task<IActionResult> GetRoles(CancellationToken cancellationToken)
    {
        var result = await mediatr.Send(new GetRolesQuery(), cancellationToken);
        return Ok(result);
    }
}