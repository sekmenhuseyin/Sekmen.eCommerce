namespace Sekmen.Commerce.Services.Email.App.EventHandlers;

public class UserCreatedEventHandler
{
    
}
public record UserCreatedEventAsync(string FullName, string Email) : IEvent;
