namespace Sekmen.Commerce.Frontend.Application.Services;

public interface ICartService
{
    Task<CartViewModel?> GetByUserIdAsync(string userId);
    Task<Result<object?>> AddOrUpdateAsync(CreateOrUpdateCartCommand command);
    Task<Result<object?>> RemoveAsync(int cartDetailsId);
    Task<Result<object?>> ApplyCouponAsync(ApplyCouponCommand command);
}

public sealed class CartService(
    HttpClient httpClient,
    AppSettingsModel appSettings,
    ITokenProviderService tokenProviderService
) : BaseService(httpClient, tokenProviderService), ICartService
{
    private readonly string _baseUrl = appSettings.Services.CartApi.Url + "api/carts/";

    public async Task<CartViewModel?> GetByUserIdAsync(string userId)
    {
        var response = await SendAsync(new RequestDto($"{_baseUrl}?userId={userId}"));

        return response.IsSuccess && !string.IsNullOrWhiteSpace(response.Value?.ToString())
            ? JsonSerializer.Deserialize<CartViewModel?>(response.Value.ToString()!, SerializerOptions)
            : default!;
    }

    public async Task<Result<object?>> AddOrUpdateAsync(CreateOrUpdateCartCommand command)
    {
        return await SendAsync(new RequestDto(_baseUrl)
        {
            HttpMethod = HttpMethod.Post,
            Data = command
        });
    }

    public async Task<Result<object?>> RemoveAsync(int cartDetailsId)
    {
        return await SendAsync(new RequestDto(_baseUrl)
        {
            HttpMethod = HttpMethod.Delete,
            Data = new { DetailsId = cartDetailsId }
        });
    }

    public async Task<Result<object?>> ApplyCouponAsync(ApplyCouponCommand command)
    {
        return await SendAsync(new RequestDto(_baseUrl + "apply-coupon")
        {
            HttpMethod = HttpMethod.Put,
            Data = command
        });
    }
}