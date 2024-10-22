// ReSharper disable ClassNeverInstantiated.Global
namespace Sekmen.Commerce.Services.Email.App.EventHandlers;

public record UserCreatedEventAsync(string FullName, string Email) : IEvent;

public class UserCreatedEventHandler(
    IEmailService emailService
) : IEventHandler<UserCreatedEventAsync>
{
    [CapSubscribe(nameof(UserCreatedEventAsync))]
    public async Task Handle(UserCreatedEventAsync notification, CancellationToken cancellationToken)
    {
        await emailService.SendAsync(notification.Email, "Welcome", "Welcome " + notification.FullName);
    }
}
