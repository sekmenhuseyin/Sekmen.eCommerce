namespace Sekmen.Commerce.Frontend.Application.Services;

public interface IAuthService
{
    Task<Result<LoginResponseViewModel?>> LoginAsync(LoginCommand command);
    Task<Result<object?>> RegisterAsync(RegisterCommand command);
}

public sealed class AuthService(
    HttpClient httpClient,
    AppSettingsModel appSettings,
    ITokenProviderService tokenProviderService
) : BaseService(httpClient, tokenProviderService), IAuthService
{
    private readonly string _baseUrl = appSettings.Services.AuthApi.Url + "api/auth/";

    public async Task<Result<LoginResponseViewModel?>> LoginAsync(LoginCommand command)
    {
        var response = await SendAsync(new RequestDto(_baseUrl + "login")
        {
            HttpMethod = HttpMethod.Post,
            Data = command
        }, false);

        return response.IsSuccess && !string.IsNullOrWhiteSpace(response.Value?.ToString())
            ? Result.Ok(JsonSerializer.Deserialize<LoginResponseViewModel?>(response.Value.ToString()!, SerializerOptions))
            : Result.Fail<LoginResponseViewModel?>(response.Error);
    }

    public async Task<Result<object?>> RegisterAsync(RegisterCommand command)
    {
        return await SendAsync(new RequestDto(_baseUrl + "register")
        {
            HttpMethod = HttpMethod.Post,
            Data = command
        }, false);
    }
}