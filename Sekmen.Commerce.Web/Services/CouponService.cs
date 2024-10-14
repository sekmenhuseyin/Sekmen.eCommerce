namespace Sekmen.Commerce.Web.Services;

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
    private readonly string _baseUrl = appSettings.Services.CouponApi.Url + "api/coupon";
    public async Task<ResponseDto?> GetAllAsync()
    {
        return await SendAsync(new RequestDto(_baseUrl + "/"));
    }

    public async Task<ResponseDto?> GetAsync(string code)
    {
        return await SendAsync(new RequestDto(_baseUrl + "/" + code));
    }

    public Task<ResponseDto?> GetAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseDto?> CreateAsync(CouponDto couponDto)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseDto?> UpdateAsync(CouponDto couponDto)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseDto?> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}