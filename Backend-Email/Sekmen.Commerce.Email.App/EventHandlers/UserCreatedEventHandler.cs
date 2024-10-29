// ReSharper disable ClassNeverInstantiated.Global

using Sekmen.Commerce.Email.App.Segregation;

namespace Sekmen.Commerce.Email.App.EventHandlers;

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
