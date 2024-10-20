namespace Sekmen.Commerce.Frontend.Web.Controllers;

public class ProductController(IProductService productService) : Controller
{
    public async Task<IActionResult> Index()
    {
        var products = await productService.GetAllAsync();
        return View(products);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ProductDto productDto)
    {
        if (!ModelState.IsValid)
            return View(productDto);

        var response = await productService.CreateAsync(productDto);
        if (response.IsSuccess)
            return RedirectToAction(nameof(Index));

        ModelState.AddModelError("CustomError", response.Error);
        return View(productDto);
    }

    public async Task<IActionResult> Edit([FromRoute] int id)
    {
        var product = await productService.GetAsync(id);
        if (product.Value is null)
            return RedirectToAction(nameof(Index));

        return View(product.Value);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(ProductDto productDto)
    {
        if (!ModelState.IsValid)
            return View(productDto);

        var response = await productService.UpdateAsync(productDto);
        if (response.IsSuccess)
            return RedirectToAction(nameof(Index));

        ModelState.AddModelError("CustomError", response.Error);
        return View(productDto);
    }

    public async Task<IActionResult> Delete(int id)
    {
        _ = await productService.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}