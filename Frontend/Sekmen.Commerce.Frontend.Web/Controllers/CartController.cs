namespace Sekmen.Commerce.Frontend.Web.Controllers;

public class CartController(ICartService cartService) : Controller
{
    [Authorize]
    public async Task<IActionResult> Index()
    {
        var cart = await cartService.GetByUserIdAsync(GetUserId());

        return View(cart);
    }

    private string GetUserId()
    {
        return User.Claims.First().Value;
    }
}