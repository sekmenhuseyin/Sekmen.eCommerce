namespace Sekmen.Commerce.Frontend.Web.Controllers;

[Authorize]
public class CartController(ICartService cartService) : Controller
{
    public async Task<IActionResult> Index()
    {
        var cart = await cartService.GetByUserIdAsync(User.Claims.First().Value);

        return View(cart);
    }

    public async Task<IActionResult> Remove(int cartDetailsId)
    {
        await cartService.RemoveAsync(cartDetailsId);

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> ApplyCoupon(CartViewModel command)
    {
        var model = new ApplyCouponCommand(User.Claims.First().Value, command.Cart.CouponCode);
        await cartService.ApplyCouponAsync(model);

        return RedirectToAction(nameof(Index));
    }
}