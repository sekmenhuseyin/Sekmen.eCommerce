using Sekmen.Commerce.Auth.Shared.Segregation;

namespace Sekmen.Commerce.Auth.Shared.Models;

public interface IEntity
{
    IEnumerable<IEvent> Events { get; }
    void ClearEvents();
    void AddEvent(IEvent @event);
}

public abstract class EntityBase : IEntity
{
    [NotMapped]
    public IEnumerable<IEvent> Events => _events.Values;

    private readonly IDictionary<Type, IEvent> _events = new Dictionary<Type, IEvent>();

    public void AddEvent(IEvent @event)
    {
        _events[@event.GetType()] = @event;
    }

    public void ClearEvents()
    {
        _events.Clear();
    }
}