using Sekmen.Commerce.Frontend.Application.Models.Auth;

namespace Sekmen.Commerce.Frontend.Web.Controllers;

public class AuthController(IAuthService authService) : Controller
{
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginCommand command)
    {
        var result = await authService.LoginAsync(command);
        if (result.IsSuccess)
        {
            return Redirect("/");
        }

        return View(command);
    }

    [HttpGet]
    public IActionResult Register()
    {
        var roleList = new List<SelectListItem>
        {
            new() { Text = RoleConstants.RoleAdmin, Value = RoleConstants.RoleAdmin },
            new() { Text = RoleConstants.RoleCustomer, Value = RoleConstants.RoleCustomer }
        };
        ViewBag.RoleList = roleList;

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterCommand command)
    {
        var result = await authService.RegisterAsync(command);
        if (result.IsSuccess)
        {
            var assignRoleCommand = new AssignRoleCommand(command.Email,
                string.IsNullOrWhiteSpace(command.Role) ? RoleConstants.RoleCustomer : command.Role);
            _ = await authService.AssignRoleAsync(assignRoleCommand);

            return RedirectToAction(nameof(Login));
        }

        var roleList = new List<SelectListItem>
        {
            new() { Text = RoleConstants.RoleAdmin, Value = RoleConstants.RoleAdmin },
            new() { Text = RoleConstants.RoleCustomer, Value = RoleConstants.RoleCustomer }
        };
        ViewBag.RoleList = roleList;

        return View(command);
    }

    [HttpGet]
    public IActionResult Logout()
    {
        return Redirect("/");
    }
}