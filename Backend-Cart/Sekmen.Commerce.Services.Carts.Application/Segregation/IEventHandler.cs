namespace Sekmen.Commerce.Services.Carts.Application.Segregation;

public interface IEventHandler<in TEvent> : INotificationHandler<TEvent>
    where TEvent : class, IEvent;