using Sekmen.Commerce.Auth.Domain.Events;

namespace Sekmen.Commerce.Auth.Domain;

public sealed class ApplicationUser : IdentityUser, IEntity
{
    public string Name { get; set; } = string.Empty;

    private ApplicationUser()
    {
    }

    public ApplicationUser(string name, string email, string phoneNumber) : this()
    {
        Name = name;
        Email = email;
        UserName = email;
        PhoneNumber = phoneNumber;
        NormalizedUserName = email.ToUpper();
        NormalizedEmail = email.ToUpper();
        AddEvent(new UserCreatedEventAsync(name, email));
    }

    [NotMapped]
    public IEnumerable<IEvent> Events => _events.Values;

    private readonly Dictionary<Type, IEvent> _events = new();

    public void AddEvent(IEvent @event)
    {
        _events[@event.GetType()] = @event;
    }

    public void ClearEvents()
    {
        _events.Clear();
    }
}