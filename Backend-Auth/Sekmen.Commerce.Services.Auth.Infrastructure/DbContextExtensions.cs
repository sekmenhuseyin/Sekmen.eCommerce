namespace Sekmen.Commerce.Services.Auth.Infrastructure;

public static class DbContextExtensions
{
    public static async Task DispatchDomainEventsAsync(this DbContext ctx, IServiceScopeFactory scopeFactory)
    {
        using var scope = scopeFactory.CreateScope();
        var publisher = scope.ServiceProvider.GetRequiredService<ICapPublisher>();

        var domainEntities = ctx.ChangeTracker
            .Entries<IEntity>()
            .Where(x => x.Entity.Events.Any()).ToArray();

        var domainEvents = domainEntities
            .SelectMany(x => x.Entity.Events)
            .ToList();

        foreach (var item in domainEntities.ToList())
            item.Entity.ClearEvents();

        var tasks = domainEvents
            .Select(async @event =>
            {
                await publisher.PublishAsync(@event.GetType().Name, @event);
            });

        await Task.WhenAll(tasks);
    }
}