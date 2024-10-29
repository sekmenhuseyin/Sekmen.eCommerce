namespace Sekmen.Commerce.Auth.Domain.Events;

public record UserCreatedEventAsync(string FullName, string Email) : IEvent;
