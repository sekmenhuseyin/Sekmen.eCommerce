﻿namespace Sekmen.Commerce.Services.Auth.Application.Segregation;

public interface IEventHandler<in TEvent> : INotificationHandler<TEvent>
    where TEvent : class, IEvent;