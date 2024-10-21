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

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CouponDto couponDto)
    {
        if (!ModelState.IsValid)
            return View(couponDto);

        var response = await couponService.CreateAsync(couponDto);
        if (response.IsSuccess)
            return RedirectToAction(nameof(Index));

        ModelState.AddModelError("CustomError", response.Error);
        return View(couponDto);
    }

    public async Task<IActionResult> Edit([FromRoute] int id)
    {
        var coupon = await couponService.GetAsync(id);
        if (coupon is null)
            return RedirectToAction(nameof(Index));

        return View(coupon);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(CouponDto couponDto)
    {
        if (!ModelState.IsValid)
            return View(couponDto);

        var response = await couponService.UpdateAsync(couponDto);
        if (response.IsSuccess)
            return RedirectToAction(nameof(Index));

        ModelState.AddModelError("CustomError", response.Error);
        return View(couponDto);
    }

    public async Task<IActionResult> Delete(int id)
    {
        await couponService.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}