namespace Sekmen.Commerce.MessageBus;

public interface IMessageBus
{
    Task PublishAsync(object message, string name);
}

internal sealed class MessageBus : IMessageBus
{
    public Task PublishAsync(object message, string name)
    {
        throw new NotImplementedException();
    }
}