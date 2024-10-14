using Sekmen.Commerce.Frontend.Application.Models;

namespace Sekmen.Commerce.Frontend.Application.Services;

public interface ICouponService
{
    Task<List<CouponDto>> GetAllAsync();
    Task<CouponDto?> GetAsync(string code);
    Task<CouponDto?> GetAsync(int id);
    Task<ResponseDto?> CreateAsync(CouponDto couponDto);
    Task<ResponseDto?> UpdateAsync(CouponDto couponDto);
    Task<ResponseDto?> DeleteAsync(int id);
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

        return response is not null && response.IsSuccess && !string.IsNullOrWhiteSpace(response.Result?.ToString())
            ? JsonSerializer.Deserialize<List<CouponDto>>(response.Result.ToString()!, SerializerOptions)!
            : [];
    }

    public async Task<CouponDto?> GetAsync(string code)
    {
        var response = await SendAsync(new RequestDto(_baseUrl + code));

        return response is not null && response.IsSuccess && !string.IsNullOrWhiteSpace(response.Result?.ToString())
            ? JsonSerializer.Deserialize<CouponDto>(response.Result.ToString()!, SerializerOptions)
            : null;
    }

    public async Task<CouponDto?> GetAsync(int id)
    {
        var response = await SendAsync(new RequestDto(_baseUrl + id));

        return response is not null && response.IsSuccess && !string.IsNullOrWhiteSpace(response.Result?.ToString())
            ? JsonSerializer.Deserialize<CouponDto>(response.Result.ToString()!, SerializerOptions)
            : null;
    }

    public async Task<ResponseDto?> CreateAsync(CouponDto couponDto)
    {
        return await SendAsync(new RequestDto(_baseUrl)
        {
            HttpMethod = HttpMethod.Post,
            Data = couponDto
        });
    }

    public async Task<ResponseDto?> UpdateAsync(CouponDto couponDto)
    {
        return await SendAsync(new RequestDto(_baseUrl)
        {
            HttpMethod = HttpMethod.Put,
            Data = couponDto
        });
    }

    public async Task<ResponseDto?> DeleteAsync(int id)
    {
        return await SendAsync(new RequestDto(_baseUrl + id)
        {
            HttpMethod = HttpMethod.Delete
        });
    }
}