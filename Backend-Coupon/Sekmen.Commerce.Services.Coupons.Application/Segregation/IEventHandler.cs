﻿namespace Sekmen.Commerce.Services.Coupons.Application.Segregation;

public interface IEventHandler<in TEvent> : INotificationHandler<TEvent>
    where TEvent : class, IEvent;