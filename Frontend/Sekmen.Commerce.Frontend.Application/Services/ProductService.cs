namespace Sekmen.Commerce.Frontend.Application.Services;

public interface IProductService
{
    Task<List<ProductDto>> GetAllAsync();
    Task<ProductDto?> GetAsync(string code);
    Task<ProductDto?> GetAsync(int id);
    Task<Result<object?>> CreateAsync(ProductDto productDto);
    Task<Result<object?>> UpdateAsync(ProductDto productDto);
    Task<Result<object?>> DeleteAsync(int id);
}

public sealed class ProductService(
    HttpClient httpClient,
    AppSettingsModel appSettings,
    ITokenProviderService tokenProviderService
) : BaseService(httpClient, tokenProviderService), IProductService
{
    private readonly string _baseUrl = appSettings.Services.ProductApi.Url + "api/product/";

    public async Task<List<ProductDto>> GetAllAsync()
    {
        var response = await SendAsync(new RequestDto(_baseUrl));

        return response.IsSuccess && !string.IsNullOrWhiteSpace(response.Value?.ToString())
            ? JsonSerializer.Deserialize<List<ProductDto>>(response.Value.ToString()!, SerializerOptions)!
            : [];
    }

    public async Task<ProductDto?> GetAsync(string code)
    {
        var response = await SendAsync(new RequestDto(_baseUrl + code));

        return response.IsSuccess && !string.IsNullOrWhiteSpace(response.Value?.ToString())
            ? JsonSerializer.Deserialize<ProductDto>(response.Value.ToString()!, SerializerOptions)
            : null;
    }

    public async Task<ProductDto?> GetAsync(int id)
    {
        var response = await SendAsync(new RequestDto(_baseUrl + id));

        return response.IsSuccess && !string.IsNullOrWhiteSpace(response.Value?.ToString())
            ? JsonSerializer.Deserialize<ProductDto>(response.Value.ToString()!, SerializerOptions)
            : null;
    }

    public async Task<Result<object?>> CreateAsync(ProductDto productDto)
    {
        return await SendAsync(new RequestDto(_baseUrl)
        {
            HttpMethod = HttpMethod.Post,
            Data = productDto
        });
    }

    public async Task<Result<object?>> UpdateAsync(ProductDto productDto)
    {
        return await SendAsync(new RequestDto(_baseUrl)
        {
            HttpMethod = HttpMethod.Put,
            Data = productDto
        });
    }

    public async Task<Result<object?>> DeleteAsync(int id)
    {
        return await SendAsync(new RequestDto(_baseUrl + id)
        {
            HttpMethod = HttpMethod.Delete
        });
    }
}