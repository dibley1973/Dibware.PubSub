namespace Dibware.PubSub.Core.HandlerResolution;

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Dibware.PubSub.Core.Contracts;
using Dibware.PubSub.Core.NotificationHandling;
using Microsoft.Extensions.DependencyInjection;

/*
 * This is all experimental and not used in the current implementation. It is here for reference and future exploration. 
 */

public interface INotificationHandlerExecutorsResolver<TNotification>
        where TNotification : INotification
{
    public IEnumerable<NotificationHandlerExecutor<TNotification>> ResolveHandlers();
}

public class OnDemandNotificationHandlerExecutorsResolver<TNotification> : INotificationHandlerExecutorsResolver<TNotification>
        where TNotification : INotification
{
    private readonly IServiceProvider _serviceProvider;

    public OnDemandNotificationHandlerExecutorsResolver(IServiceProvider serviceProvider) =>
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));

    public IEnumerable<NotificationHandlerExecutor<TNotification>> ResolveHandlers()
    {
        var notificationHandlers = _serviceProvider
            .GetRequiredService<IEnumerable<INotificationHandler<TNotification>>>();

        var notificationHandlerExecuters = notificationHandlers
            .Select(handler =>
            {
                Func<TNotification, CancellationToken, Task> handlerCallback = (notification, cancellationToken) => handler.Handle(notification, cancellationToken);
                return new NotificationHandlerExecutor<TNotification>(handler, handlerCallback);
            })
            .ToArray();

        return notificationHandlerExecuters;
    }
}

public class CachedNotificationHandlerExecutorsResolver<TNotification> : INotificationHandlerExecutorsResolver<TNotification>
        where TNotification : INotification
{
    private readonly IServiceProvider _serviceProvider;
    private ConcurrentDictionary<Type, NotificationHandlerExecutor<TNotification>[]> _notificationHandlerCache = new();

    public CachedNotificationHandlerExecutorsResolver(IServiceProvider serviceProvider) =>
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));

    public IEnumerable<NotificationHandlerExecutor<TNotification>> ResolveHandlers() //where TNotification : INotification
    {
        var a = _notificationHandlerCache.GetOrAdd(typeof(TNotification), notificationType =>
        {
            var notificationHandlers = _serviceProvider
                .GetRequiredService<IEnumerable<INotificationHandler<TNotification>>>();

            var notificationHandlerExecuters = notificationHandlers
                .Select(handler =>
                {
                    Func<TNotification, CancellationToken, Task> handlerCallback = (notification, cancellationToken) => handler.Handle(notification, cancellationToken);
                    return new NotificationHandlerExecutor<TNotification>(handler, handlerCallback);
                })
                .ToArray();
            return notificationHandlerExecuters;
        });

        return a;
    }
}
