namespace Sekmen.Commerce.Frontend.Application.Services;

public class BaseService(
    HttpClient httpClient
)
{
    protected readonly JsonSerializerOptions SerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    protected async Task<Result<object?>> SendAsync(RequestDto requestDto)
    {
        var message = new HttpRequestMessage(requestDto.HttpMethod, new Uri(requestDto.Url));
        message.Headers.Add("Accept", "application/json");

        if (requestDto.Data is not null)
        {
            message.Content = new StringContent(JsonSerializer.Serialize(requestDto.Data), Encoding.UTF8, "application/json");
        }

        var apiResponse = await httpClient.SendAsync(message);

        if (apiResponse.StatusCode != HttpStatusCode.OK) 
            return Result.Fail(apiResponse.StatusCode.ToString());

        var apiContent = await apiResponse.Content.ReadAsStringAsync();
        var apiModel = JsonSerializer.Deserialize<ResponseDto>(apiContent, SerializerOptions)!;
        return Result.Ok(apiModel.Value);
    }
}