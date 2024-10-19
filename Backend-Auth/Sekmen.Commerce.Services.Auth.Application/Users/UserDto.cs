namespace Sekmen.Commerce.Services.Auth.Application.Users;

public record UserDto
{
    public string Id { get; init; }
    public string Name { get; init; }
    public string Email { get; init; }
    public string PhoneNumber { get; init; }
    public string Role { get; init; }
}