namespace Dibware.PubSub.Core.Contracts;

/// <summary>
/// Defines a contract for publishing a notification to one or moreregistered handlers.
/// </summary>
public interface INotificationPublisher
{
    /// <summary>
    /// Publishes a notification to one or more registered handlers.
    /// </summary>
    /// <param name="handlerExecutors">
    /// A collection of registered handlers that will be invoked to handle the notification.
    /// </param>
    /// <param name="notification">
    /// The notification to be published to the registered handlers.
    /// </param>
    /// <param name="cancellationToken">
    /// A token to cancel the asynchronous operation.
    /// </param>
    /// <returns></returns>
    Task Publish(
        IEnumerable<INotificationHandler<INotification>> handlerExecutors,
        INotification notification,
        CancellationToken cancellationToken);
}
