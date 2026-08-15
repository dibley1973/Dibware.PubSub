namespace Dibware.PubSub.Core.Contracts;

using Dibware.PubSub.Core.NotificationHandling;

/// <summary>
/// Awaits each notification handler sequentially, in a single foreach loop:
/// <code>
/// foreach (var handler in handlers) {
///     await handler(notification, cancellationToken);
/// }
/// </code>
/// </summary>
public class SequentialPublisher : INotificationPublisher
{
    /// <summary>
    /// Publishes a notification to one or more registered handlers by awaiting each handler in a single foreach loop.
    /// </summary>
    /// <param name="notificationHandlerExecuters">
    /// A collection of registered handlers (wrapped in an executor) that will be invoked to handle the notification.
    /// </param>
    /// <param name="notification">
    /// The notification to be published to the registered handlers.
    /// </param>
    /// <param name="cancellationToken">
    /// A token to cancel the asynchronous operation.
    /// </param>
    /// <returns>
    /// Represents the asynchronous operation of publishing the notification to the registered handlers.
    /// </returns>
    public async Task Publish<TNotification>(
        IEnumerable<NotificationHandlerExecutor<TNotification>> notificationHandlerExecuters,
        INotification notification,
        CancellationToken cancellationToken)
            where TNotification : INotification
    {
        foreach (var handler in notificationHandlerExecuters)
        {
            if (cancellationToken.IsCancellationRequested)
                break; // Dont handle any more notifications if cancellation is requested

            await handler.HandlerCallback((TNotification)notification, cancellationToken).ConfigureAwait(false);
        }
    }
}
