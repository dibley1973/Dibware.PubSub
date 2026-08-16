namespace Dibware.PubSub.Core.Contracts;

using Dibware.PubSub.Core.NotificationHandling;

/// <summary>
/// Defines a contract for publishing a notification to one or moreregistered handlers.
/// </summary>
public interface INotificationPublisher
{
    /// <summary>
    /// Publishes a notification to one or more registered handlers.
    /// </summary>
    /// <typeparam name="TNotification">
    /// The type of notification to be published. Must implement the <see cref="INotification"/> interface.
    /// </typeparam>
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
    Task Publish<TNotification>(
        NotificationHandlerExecutor<TNotification>[] notificationHandlerExecuters,
        TNotification notification,
        CancellationToken cancellationToken)
            where TNotification : INotification;
}
