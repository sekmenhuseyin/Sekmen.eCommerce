namespace Sekmen.Commerce.Frontend.Application.Services;

public class BaseService(
    HttpClient httpClient,
    ITokenProviderService tokenProviderService
)
{
    protected readonly JsonSerializerOptions SerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    protected async Task<Result<object?>> SendAsync(RequestDto requestDto, bool withBearer = true)
    {
        var message = new HttpRequestMessage(requestDto.HttpMethod, new Uri(requestDto.Url));
        message.Headers.Add("Accept", "application/json");

        if (withBearer)
        {
            message.Headers.Add("Authorization", $"Bearer {tokenProviderService.GetToken()}");
        }
        if (requestDto.Data is not null)
        {
            message.Content = new StringContent(JsonSerializer.Serialize(requestDto.Data), Encoding.UTF8, "application/json");
        }

        var apiResponse = await httpClient.SendAsync(message);

        if (apiResponse.StatusCode != HttpStatusCode.OK) 
            return Result.Fail<object?>(apiResponse.StatusCode.ToString());

        var apiContent = await apiResponse.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<Result<object?>>(apiContent, SerializerOptions)!;
    }
}