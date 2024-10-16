namespace Sekmen.Commerce.Frontend.Application.Models;

public record ResponseDto
{
    public object? Value { get; init; }
    public bool IsFailed { get; init; }
    public bool IsSuccess { get; init; }
    public object[]? Errors { get; init; }
}