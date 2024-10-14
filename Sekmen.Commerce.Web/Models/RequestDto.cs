namespace Sekmen.Commerce.Web.Models;

public record RequestDto(string Url)
{
    public HttpMethod HttpMethod { get; set; } = HttpMethod.Get;
    public object? Data { get; set; }
    public string Token { get; set; }
}