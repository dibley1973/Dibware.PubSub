namespace Dibware.PubSub.Core.Contracts;

/// <summary>
/// Defines the expected members of a component which takes subsriptions.
/// </summary>
public interface ISubscriber
{
    /// <summary>
    /// Subscribes the specified handler.
    /// </summary>
    /// <typeparam name="TNotification">
    /// The type of the notification. Must implement <see cref="INotification"/>.
    /// </typeparam>
    /// <param name="handler">
    /// The handler to subscribe.
    /// </param>
    public void Subscribe<TNotification>(INotificationHandler<TNotification> handler)
        where TNotification : INotification;

    /// <summary>
    /// Unsubscribes the specified handler.
    /// </summary>
    /// <typeparam name="TNotification">
    /// The type of the notification. Must implement <see cref="INotification"/>.
    /// </typeparam>
    /// <param name="handler">
    /// The handler to unsubscribe.
    /// </param>
    public void Unsubscribe<TNotification>(INotificationHandler<TNotification> handler)
        where TNotification : INotification;
}
