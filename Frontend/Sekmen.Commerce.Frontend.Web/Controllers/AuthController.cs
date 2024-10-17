namespace Sekmen.Commerce.Frontend.Web.Controllers;

public class AuthController(
    IAuthService authService,
    ITokenProviderService tokenProviderService
) : Controller
{
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpGet("access-denied")]
    public IActionResult AccessDenied()
    {
        return View(nameof(Login));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginCommand command)
    {
        var result = await authService.LoginAsync(command);
        if (result.IsSuccess && !string.IsNullOrWhiteSpace(result.Value?.Token))
        {
            await SignInUser(result.Value.Token);
            tokenProviderService.SetToken(result.Value.Token);
            return RedirectToAction(nameof(Index), HomeController.Name);
        }

        ModelState.AddModelError("CustomError", result.Errors.First().Message);
        return View(command);
    }

    [HttpGet]
    public IActionResult Register()
    {
        ViewBag.RoleList = GenerateRoles();

        return View();
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterCommand command)
    {
        var result = await authService.RegisterAsync(command);
        if (result.IsSuccess)
        {
            var assignRoleCommand = new AssignRoleCommand(command.Email,
                string.IsNullOrWhiteSpace(command.Role) ? AuthConstants.RoleCustomer : command.Role);
            _ = await authService.AssignRoleAsync(assignRoleCommand);

            return RedirectToAction(nameof(Login));
        }

        ViewBag.RoleList = GenerateRoles();

        return View(command);
    }

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        tokenProviderService.ClearToken();
        return RedirectToAction(nameof(Index), HomeController.Name);
    }

    private async Task SignInUser(string token)
    {
        var claims = new JwtSecurityTokenHandler().ReadJwtToken(token).Claims.ToArray();
        var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
        var newClaims =new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, GetValue(claims, JwtRegisteredClaimNames.Sub)),
            new Claim(JwtRegisteredClaimNames.Name, GetValue(claims, JwtRegisteredClaimNames.Name)),
            new Claim(JwtRegisteredClaimNames.Email, GetValue(claims, JwtRegisteredClaimNames.Email)),
            new Claim(ClaimTypes.Name, GetValue(claims, JwtRegisteredClaimNames.Email)),
            new Claim(ClaimTypes.Role, GetValue(claims, "role"))
        };
        identity.AddClaims(newClaims);

        var principal = new ClaimsPrincipal(identity);
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
    }

    private static string GetValue(Claim[] claims, string type)
    {
        return claims.FirstOrDefault(m => m.Type == type)?.Value ?? string.Empty;
    }

    private static List<SelectListItem> GenerateRoles()
    {
        var roleList = new List<SelectListItem>
        {
            new() { Text = AuthConstants.RoleAdmin, Value = AuthConstants.RoleAdmin },
            new() { Text = AuthConstants.RoleCustomer, Value = AuthConstants.RoleCustomer }
        };
        return roleList;
    }
}