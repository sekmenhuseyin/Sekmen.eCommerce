namespace Sekmen.Commerce.Frontend.Application.Services;

public interface ICouponService
{
    Task<PagedQueryResult<List<CouponDto>>> GetAllAsync();
    Task<Result<CouponDto?>> GetAsync(string code);
    Task<Result<CouponDto?>> GetAsync(int id);
    Task<Result<object?>> CreateAsync(CouponDto couponDto);
    Task<Result<object?>> UpdateAsync(CouponDto couponDto);
    Task<Result<object?>> DeleteAsync(int id);
}

public sealed class CouponService(
    HttpClient httpClient,
    AppSettingsModel appSettings,
    ITokenProviderService tokenProviderService
) : BaseService(httpClient, tokenProviderService), ICouponService
{
    private readonly string _baseUrl = appSettings.Services.CouponApi.Url + "api/coupons/";

    public async Task<PagedQueryResult<List<CouponDto>>> GetAllAsync()
    {
        var response = await SendAsync(new RequestDto(_baseUrl));

        return response.IsSuccess && !string.IsNullOrWhiteSpace(response.Value?.ToString())
            ? JsonSerializer.Deserialize<PagedQueryResult<List<CouponDto>>>(response.Value.ToString()!, SerializerOptions)!
            : default!;
    }

    public async Task<Result<CouponDto?>> GetAsync(string code)
    {
        var response = await SendAsync(new RequestDto(_baseUrl + code));

        return response.IsSuccess && !string.IsNullOrWhiteSpace(response.Value?.ToString())
            ? JsonSerializer.Deserialize<Result<CouponDto?>>(response.Value.ToString()!, SerializerOptions)!
            : default!;
    }

    public async Task<Result<CouponDto?>> GetAsync(int id)
    {
        var response = await SendAsync(new RequestDto(_baseUrl + id));

        return response.IsSuccess && !string.IsNullOrWhiteSpace(response.Value?.ToString())
            ? JsonSerializer.Deserialize<Result<CouponDto?>>(response.Value.ToString()!, SerializerOptions)!
            : default!;
    }

    public async Task<Result<object?>> CreateAsync(CouponDto couponDto)
    {
        return await SendAsync(new RequestDto(_baseUrl)
        {
            HttpMethod = HttpMethod.Post,
            Data = couponDto
        });
    }

    public async Task<Result<object?>> UpdateAsync(CouponDto couponDto)
    {
        return await SendAsync(new RequestDto(_baseUrl)
        {
            HttpMethod = HttpMethod.Put,
            Data = couponDto
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