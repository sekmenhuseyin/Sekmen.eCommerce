namespace Sekmen.Commerce.Web.Models;

public record ResponseDto
{
    public object? Result { get; init; }
    public bool IsSuccess { get; init; }
    public string? ErrorMessage { get; init; }
}