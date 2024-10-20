using System.Text.Json;

namespace Sekmen.Commerce.Services.Carts.Application.Services;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetProducts();
}

public sealed class ProductService(HttpClient client) : IProductService
{
    public async Task<IEnumerable<ProductDto>> GetProducts()
    {
        var response = await client.GetAsync($"api/products");
        if (!response.IsSuccessStatusCode)
            return [];

        var apiContent = await response.Content.ReadAsStringAsync();
        var dto = JsonSerializer.Deserialize<Result<object>>(apiContent)!;
        if (!dto.Success || string.IsNullOrEmpty(dto.Value.ToString()))
            return [];

        return JsonSerializer.Deserialize<IEnumerable<ProductDto>>(dto.Value.ToString()!)!;
    }
}