namespace Sekmen.Commerce.Frontend.Application.Models;

public class AppSettingsModel
{
    public ServicesModel Services { get; init; } = new();
}

public class ServicesModel
{
    public UrlModel AuthApi { get; init; } = new();
    public UrlModel CouponApi { get; init; } = new();
    public UrlModel ProductApi { get; init; } = new();
}

public class UrlModel
{
    public string Url { get; init; } = string.Empty;
}