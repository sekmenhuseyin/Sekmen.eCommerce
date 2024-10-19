namespace Sekmen.Commerce.Frontend.Application.Models.Auth;

public record RegisterCommand
{
    [Required] public string Email { get; init; }
    [Required] public string Name { get; init; }
    [Required] public string PhoneNumber { get; init; }
    [Required] public string Password { get; init; }
    [Required] public string Role { get; init; }
}