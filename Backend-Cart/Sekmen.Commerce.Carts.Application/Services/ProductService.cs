using System.Text.Json;
using Sekmen.Commerce.Carts.Application.Models;
using Sekmen.Commerce.Carts.Application.Products;

namespace Sekmen.Commerce.Carts.Application.Services;

public interface IProductService
{
    Task<ProductDto[]> GetProducts(IEnumerable<int> ids);
}

public sealed class ProductService(HttpClient client) : IProductService
{
    private readonly JsonSerializerOptions _serializerOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public async Task<ProductDto[]> GetProducts(IEnumerable<int> ids)
    {
        var url = "api/products/some?ids=" + string.Join("&ids=", ids);
        var response = await client.GetAsync(url);
        if (!response.IsSuccessStatusCode)
            return [];

        var apiContent = await response.Content.ReadAsStringAsync();
        var dto = JsonSerializer.Deserialize<Result<ProductDto[]>>(apiContent, _serializerOptions);
        if (dto is null || string.IsNullOrEmpty(dto.Value.ToString()))
            return [];

        return dto.Value;
    }
}