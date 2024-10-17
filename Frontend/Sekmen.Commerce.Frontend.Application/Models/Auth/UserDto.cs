namespace Sekmen.Commerce.Frontend.Application.Models.Auth;

public record UserDto
{
    public string Id { get; init; }
    public string Name { get; init; }
    public string Email { get; init; }
    public string PhoneNumber { get; init; }
}