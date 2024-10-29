namespace Sekmen.Commerce.Email.App.Segregation;

public interface IEventHandler<in TEvent> : ICapSubscribe where TEvent : IEvent
{
    public Task Handle(TEvent notification, CancellationToken cancellationToken);
}