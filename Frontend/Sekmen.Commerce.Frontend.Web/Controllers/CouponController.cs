namespace Sekmen.Commerce.Frontend.Web.Controllers;

public class CouponController(ICouponService couponService) : Controller
{
    public async Task<IActionResult> Index()
    {
        var coupons = await couponService.GetAllAsync();
        return View(coupons);
    }
}