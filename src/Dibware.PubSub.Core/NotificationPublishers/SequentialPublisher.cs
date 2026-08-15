namespace Dibware.PubSub.Core.Contracts;

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
    /// <param name="handlers">
    /// A collection of registered handlers that will be invoked to handle the notification.
    /// </param>
    /// <param name="notification">
    /// The notification to be published to the registered handlers.
    /// </param>
    /// <param name="cancellationToken">
    /// A token to cancel the asynchronous operation.
    /// </param>
    /// <returns></returns>
    public async Task Publish(IEnumerable<INotificationHandler<INotification>> handlers, INotification notification, CancellationToken cancellationToken)
    {
        foreach (var handler in handlers)
        {
            if (cancellationToken.IsCancellationRequested)
                break; // Dont handle any more notifications if cancellation is requested

            await handler.Handle(notification, cancellationToken).ConfigureAwait(false);
        }
    }
}
