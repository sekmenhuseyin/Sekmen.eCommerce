namespace Sekmen.Commerce.Frontend.Application.Models.Auth;

public record RegisterCommand(string Email, string Name, string PhoneNumber, string Password, string? Role);