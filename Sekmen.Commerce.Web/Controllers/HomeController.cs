namespace Sekmen.Commerce.Web.Controllers;

public class HomeController(ILogger<HomeController> logger, ICouponService couponService) : Controller
{
    public async Task<IActionResult> Index()
    {
        var result = await couponService.GetAllAsync();
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}