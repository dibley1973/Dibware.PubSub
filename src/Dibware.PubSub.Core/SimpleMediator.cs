namespace Dibware.PubSub.Core;

using Dibware.PubSub.Core.Contracts;
using Dibware.PubSub.Core.NotificationHandling;
using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// A simple mediator implementation that allows for publishing notifications to registered handlers.
/// </summary>
/// <remarks>
/// Reference repositories for inspiration and implementation details:
/// Ref: https://github.com/hasanxdev/DispatchR/blob/main/src/DispatchR/Mediator.cs
/// Ref: https://github.com/LuckyPennySoftware/MediatR/blob/main/src/MediatR/Mediator.cs
/// </remarks>
public class SimpleMediator : ISimpleMediator
{
    private readonly IServiceProvider _serviceProvider;
    private readonly INotificationPublisher _notificationPublisher;

    /// <summary>
    /// Initializes a new instance of the <see cref="SimpleMediator"/> class.
    /// </summary>
    /// <param name="serviceProvider">Service provider. Can be a scoped or root provider</param>
    /// <param name="notificationPublisher">The notification publisher.</param>
    /// <exception cref="ArgumentNullException">Thrown when either parameter is null.</exception>
    public SimpleMediator(IServiceProvider serviceProvider, INotificationPublisher notificationPublisher)
    {
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        _notificationPublisher = notificationPublisher ?? throw new ArgumentNullException(nameof(notificationPublisher));
    }

    /// <summary>
    /// Publishes a notification to all registered handlers.
    /// </summary>
    /// <typeparam name="TNotification">The type of the notification.</typeparam>
    /// <param name="notification">The notification to publish.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <exception cref="NotImplementedException">Thrown when the method is not implemented.</exception>
    public async Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default)
        where TNotification : INotification
    {
        var notificationHandlers = _serviceProvider
            .GetRequiredService<IEnumerable<INotificationHandler<TNotification>>>();

        // TODO: Investigate if we can cache the handlers to avoid resolving them every time.
        // This could improve performance, especially if the same notification type is published frequently.
        // We might need to create a warapper class for this due to generics.
        //var handlerWrapper = _notificationHandlerCache.GetOrAdd(typeof(TNotification), static notificationType =>
        //{
        //    var wrapperType = typeof(NotificationHandlerWrapperImpl<>).MakeGenericType(notificationType);
        //    var wrapper = Activator.CreateInstance(wrapperType) ??
        //        throw new InvalidOperationException($"Could not create wrapper for type {notificationType}");
        //    return (NotificationHandlerWrapper)wrapper;
        //});

        // TODO:Investigate if using Unsafe.As below is actully needed in our implementation.
        // It is used in the DispatchR implementation, but we are not sure if it is needed in our case.
        //var unSafeHandlers = Unsafe.As<INotificationHandler<TNotification>[]>(handlers);

        var notificationHandlerExecuters = notificationHandlers
            .Select(handler =>
            {
                Func<TNotification, CancellationToken, Task> handlerCallback = (notification, cancellationToken) => handler.Handle(notification, cancellationToken);
                return new NotificationHandlerExecutor<TNotification>(handler, handlerCallback);
            })
            .ToArray();

        await _notificationPublisher.Publish<TNotification>(notificationHandlerExecuters, notification, cancellationToken);
    }

    //private ConcurrentDictionary<Type, NotificationHandlerWrapper> _notificationHandlerCache = new ConcurrentDictionary<Type, NotificationHandlerWrapper>();
}

//public abstract class NotificationHandlerWrapper
//{
//    public abstract Task Handle(INotification notification, CancellationToken cancellationToken);
//}

//public class NotificationHandlerWrapperImpl<TNotification> : NotificationHandlerWrapper
//    where TNotification : INotification, new()
//{
//    public INotificationHandler<TNotification> Handler { get; }

//    public NotificationHandlerWrapperImpl(INotificationHandler<TNotification> handler)
//    {
//        Handler = handler ?? throw new ArgumentNullException(nameof(handler));
//    }

//    public override Task Handle(INotification notification, CancellationToken cancellationToken)
//    {
//        return Handler.Handle((TNotification)notification, cancellationToken);
//    }
//}
