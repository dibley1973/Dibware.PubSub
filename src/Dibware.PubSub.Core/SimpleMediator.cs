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

        // CACHING
        // I did spend a fair few hours of my life investigating if we can cache the handlers to
        // avoid resolving them every time. This could improve performance, especially if the same
        // notification type is published highly frequently.
        // I investigated many avenues but due to generics each one edded up at a brick wall.
        // I mainly focused around various "wrapper classes" for this due to generics.
        //
        // In the end I have decided to forgo caching until it is proved in the future that
        // service location is indeed a bottleneck. I'd like to hope that Microsoft have already
        // built out as much performance gains as they can from the DependencyInjection components.

        NotificationHandlerExecutor<TNotification>[] notificationHandlerExecuters = notificationHandlers
            .Select(handler =>
            {
                Func<TNotification, CancellationToken, Task> handlerCallback = (notification, cancellationToken) => handler.Handle(notification, cancellationToken);
                return new NotificationHandlerExecutor<TNotification>(handler, handlerCallback);
            })
            .ToArray();

        await _notificationPublisher.Publish(notificationHandlerExecuters, notification, cancellationToken);
    }
}
