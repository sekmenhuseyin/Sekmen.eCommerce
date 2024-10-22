namespace Sekmen.Commerce.Services.Auth.Shared.Models;

public interface IEntity
{
    IEnumerable<INotification> Events { get; }
    void ClearEvents();
    void AddEvent(INotification @event);
}

public abstract class EntityBase : IEntity
{
    [NotMapped]
    public IEnumerable<INotification> Events => _events.Values;

    private readonly IDictionary<Type, INotification> _events = new Dictionary<Type, INotification>();

    public void AddEvent(INotification @event)
    {
        _events[@event.GetType()] = @event;
    }

    public void ClearEvents()
    {
        _events.Clear();
    }
}