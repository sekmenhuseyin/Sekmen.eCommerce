namespace Sekmen.Commerce.Web.Models;

public class AppSettingsModel
{
    public ServicesModel Services { get; init; } = new();
}

public class ServicesModel
{
    public UrlModel CouponApi { get; init; } = new();
}

public class UrlModel
{
    public string Url { get; init; } = string.Empty;
}