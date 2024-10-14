namespace Sekmen.Commerce.Web.Models;

public record RequestDto
{
    public string HttpMethod { get; set; } = HttpMethods.Get;
    public string Url { get; set; }
    public object Data { get; set; }
    public string Token { get; set; }
}