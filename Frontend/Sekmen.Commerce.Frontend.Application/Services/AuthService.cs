namespace Sekmen.Commerce.Frontend.Application.Services;

public interface IAuthService
{
    Task<Result<LoginResponseViewModel?>> LoginAsync(LoginCommand command);
    Task<Result<object?>> RegisterAsync(RegisterCommand command);
    Task<Result<object?>> AssignRoleAsync(AssignRoleCommand command);
}

public sealed class AuthService(
    HttpClient httpClient,
    AppSettingsModel appSettings
) : BaseService(httpClient), IAuthService
{
    private readonly string _baseUrl = appSettings.Services.AuthApi.Url + "api/auth/";

    public async Task<Result<LoginResponseViewModel?>> LoginAsync(LoginCommand command)
    {
        var response = await SendAsync(new RequestDto(_baseUrl + "login")
        {
            HttpMethod = HttpMethod.Post,
            Data = command
        });

        return response.IsSuccess && !string.IsNullOrWhiteSpace(response.Value?.ToString())
            ? JsonSerializer.Deserialize<LoginResponseViewModel>(response.Value.ToString()!, SerializerOptions)
            : null;
    }

    public async Task<Result<object?>> RegisterAsync(RegisterCommand command)
    {
        return await SendAsync(new RequestDto(_baseUrl + "register")
        {
            HttpMethod = HttpMethod.Post,
            Data = command
        });
    }

    public async Task<Result<object?>> AssignRoleAsync(AssignRoleCommand command)
    {
        return await SendAsync(new RequestDto(_baseUrl + "assign-role")
        {
            HttpMethod = HttpMethod.Post,
            Data = command
        });
    }
}