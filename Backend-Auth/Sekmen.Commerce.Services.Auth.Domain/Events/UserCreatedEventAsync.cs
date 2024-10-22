namespace Sekmen.Commerce.Services.Auth.Domain.Events;

public record UserCreatedEventAsync(string FullName, string Email) : IEvent;
