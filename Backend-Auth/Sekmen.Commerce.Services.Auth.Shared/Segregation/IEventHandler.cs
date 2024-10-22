namespace Sekmen.Commerce.Services.Auth.Shared.Segregation;

public interface IEventHandler<in TEvent> : INotificationHandler<TEvent>
    where TEvent : class, IEvent;