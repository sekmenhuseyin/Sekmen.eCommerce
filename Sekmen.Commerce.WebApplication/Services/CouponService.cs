namespace Sekmen.Commerce.WebApplication.Services;

public interface ICouponService
{
    Task<ResponseDto?> GetAllAsync();
    Task<ResponseDto?> GetAsync(string code);
    Task<ResponseDto?> GetAsync(int id);
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
    public async Task<ResponseDto?> GetAllAsync()
    {
        return await SendAsync(new RequestDto(_baseUrl));
    }

    public async Task<ResponseDto?> GetAsync(string code)
    {
        return await SendAsync(new RequestDto(_baseUrl + code));
    }

    public async Task<ResponseDto?> GetAsync(int id)
    {
        return await SendAsync(new RequestDto(_baseUrl + id));
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