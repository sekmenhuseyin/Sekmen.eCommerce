namespace Sekmen.Commerce.Frontend.Web.Controllers;

public class CartController(ICartService cartService) : Controller
{
    [Authorize]
    public async Task<IActionResult> Index()
    {
        var cart = await cartService.GetByUserIdAsync(User.Claims.First().Value);

        return View(cart);
    }
}