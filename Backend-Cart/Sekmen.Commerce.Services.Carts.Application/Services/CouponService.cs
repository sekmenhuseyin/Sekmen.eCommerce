using System.Text.Json;

namespace Sekmen.Commerce.Services.Carts.Application.Services;

public interface ICouponService
{
    Task<CouponDto> GetCoupon(string code);
}

public sealed class CouponService(HttpClient client) : ICouponService
{
    private readonly JsonSerializerOptions _serializerOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public async Task<CouponDto> GetCoupon(string code)
    {
        var url = "api/coupons/" + code;
        var response = await client.GetAsync(url);
        if (!response.IsSuccessStatusCode)
            return default!;

        var apiContent = await response.Content.ReadAsStringAsync();
        var dto = JsonSerializer.Deserialize<Result<CouponDto>>(apiContent, _serializerOptions);
        if (dto is null || string.IsNullOrEmpty(dto.Value.ToString()))
            return default!;

        return dto.Value;
    }
}