namespace Sekmen.Commerce.Email.App;

public class AppSettingsModel
{
    public JwtOptions JwtOptions { get; init; } = new();
    public RabbitMqSettings RabbitMqS { get; init; } = new();
}

public class JwtOptions
{
    public string Secret { get; init; } = string.Empty;
    public int Expire { get; init; } = 7;
    public string Issuer { get; init; } = string.Empty;
    public string Audience { get; init; } = string.Empty;
}

public class RabbitMqSettings
{
    public const string SettingPath = "RabbitMQ";
    public string Connection { get; init; } = string.Empty;
    public string Username { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
    public string QueueName { get; init; } = string.Empty;
    public string TopicPrefix { get; init; } = string.Empty;
    public bool Disabled { get; init; }
}