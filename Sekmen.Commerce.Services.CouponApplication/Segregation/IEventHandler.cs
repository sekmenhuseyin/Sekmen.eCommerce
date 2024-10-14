namespace Sekmen.Commerce.Services.CouponApplication.Segregation;

public interface IEventHandler<in TEvent> : MediatR.INotificationHandler<TEvent>
    where TEvent : class, IEvent;