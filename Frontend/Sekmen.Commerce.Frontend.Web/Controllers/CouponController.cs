namespace Sekmen.Commerce.Frontend.Web.Controllers;

public class CouponController(ICouponService couponService) : Controller
{
    public async Task<IActionResult> Index()
    {
        var coupons = await couponService.GetAllAsync();
        return View(coupons);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CouponDto couponDto)
    {
        if (!ModelState.IsValid)
            return View(couponDto);

        var response = await couponService.CreateAsync(couponDto);
        if (response is not null && response.IsSuccess)
        {
            return RedirectToAction(nameof(Index));
        }

        return View(couponDto);
    }
}