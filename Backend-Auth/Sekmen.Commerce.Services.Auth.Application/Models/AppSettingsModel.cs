namespace Sekmen.Commerce.Services.Auth.Application.Models;

public class AppSettingsModel
{
    public JwtOptions JwtOptions { get; init; } = new();
    public PasswordOptionsModel PasswordOptions { get; init; } = new();
}

public class JwtOptions
{
    public string Secret { get; init; } = string.Empty;
    public int Expire { get; init; }
    public string Issuer { get; init; } = string.Empty;
    public string Audience { get; init; } = string.Empty;
}

public class PasswordOptionsModel : PasswordOptions
{
    public int ChangePeriodInDays { get; init; }
    public int MaxFailedAttempts { get; init; }
    public int LastPasswordCount { get; init; }
}
