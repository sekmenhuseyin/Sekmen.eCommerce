namespace Sekmen.Commerce.Frontend.Web.Controllers;

public class HomeController(
    IProductService productService,
    ICartService cartService
) : Controller
{
    public const string Name = "Home";

    public async Task<IActionResult> Index()
    {
        var products = await productService.GetAllAsync();
        return View(products);
    }

    public async Task<IActionResult> ProductDetails(int productId)
    {
        var response = await productService.GetAsync(productId);

        if (response is not null )
        {
            return View(response);
        }

        return NotFound();
    }

    [HttpPost, Authorize]
    public async Task<IActionResult> ProductDetails(ProductDto productDto)
    {
        var response = await cartService.AddOrUpdateAsync(
            new CreateOrUpdateCartCommand(User.Claims.First().Value, productDto.Id, productDto.Count)
        );

        return response.IsSuccess
            ? RedirectToAction(nameof(Index))
            : RedirectToAction(nameof(ProductDetails), new { productId = productDto.Id });
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