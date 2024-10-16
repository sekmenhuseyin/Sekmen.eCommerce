namespace Sekmen.Commerce.Frontend.Application.Services;

public interface ICouponService
{
    Task<List<CouponDto>> GetAllAsync();
    Task<CouponDto?> GetAsync(string code);
    Task<CouponDto?> GetAsync(int id);
    Task<Result<object?>> CreateAsync(CouponDto couponDto);
    Task<Result<object?>> UpdateAsync(CouponDto couponDto);
    Task<Result<object?>> DeleteAsync(int id);
}

public class CouponService(
    HttpClient httpClient,
    AppSettingsModel appSettings
) : BaseService(httpClient), ICouponService
{
    private readonly string _baseUrl = appSettings.Services.CouponApi.Url + "api/coupon/";

    public async Task<List<CouponDto>> GetAllAsync()
    {
        var response = await SendAsync(new RequestDto(_baseUrl));

        return response.IsSuccess && !string.IsNullOrWhiteSpace(response.Value?.ToString())
            ? JsonSerializer.Deserialize<List<CouponDto>>(response.Value.ToString()!, SerializerOptions)!
            : [];
    }

    public async Task<CouponDto?> GetAsync(string code)
    {
        var response = await SendAsync(new RequestDto(_baseUrl + code));

        return response.IsSuccess && !string.IsNullOrWhiteSpace(response.Value?.ToString())
            ? JsonSerializer.Deserialize<CouponDto>(response.Value.ToString()!, SerializerOptions)
            : null;
    }

    public async Task<CouponDto?> GetAsync(int id)
    {
        var response = await SendAsync(new RequestDto(_baseUrl + id));

        return response.IsSuccess && !string.IsNullOrWhiteSpace(response.Value?.ToString())
            ? JsonSerializer.Deserialize<CouponDto>(response.Value.ToString()!, SerializerOptions)
            : null;
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