namespace Sekmen.Commerce.Services.Auth.Application.Models;

public class JwtOptions
{
    public string Secret { get; init; } = string.Empty;
    public string Expire { get; init; } = string.Empty;
    public string Issuer { get; init; } = string.Empty;
    public string Audience { get; init; } = string.Empty;
}