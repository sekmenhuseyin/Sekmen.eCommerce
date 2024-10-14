namespace Sekmen.Commerce.Frontend.Application.Services;

public class BaseService(
    HttpClient httpClient
)
{
    protected readonly JsonSerializerOptions SerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public async Task<ResponseDto?> SendAsync(RequestDto requestDto)
    {
        var message = new HttpRequestMessage(requestDto.HttpMethod, new Uri(requestDto.Url));
        message.Headers.Add("Accept", "application/json");

        if (requestDto.Data is not null)
        {
            message.Content = new StringContent(JsonSerializer.Serialize(requestDto.Data), Encoding.UTF8, "application/json");
        }

        var apiResponse = await httpClient.SendAsync(message);

        // ReSharper disable once SwitchStatementHandlesSomeKnownEnumValuesWithDefault
        switch (apiResponse.StatusCode)
        {
            case HttpStatusCode.NotFound:
                return new ResponseDto().NotFound();
            case HttpStatusCode.Forbidden:
                return new ResponseDto().Error("Forbidden");
            case HttpStatusCode.Unauthorized:
                return new ResponseDto().Error("Unauthorized");
            case HttpStatusCode.InternalServerError:
                return new ResponseDto().Error("InternalServerError");
            default:
                var apiContent = await apiResponse.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<ResponseDto>(apiContent, SerializerOptions);
        }
    }
}