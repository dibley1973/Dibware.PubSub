namespace Dibware.PubSub.Core.Contracts;

/// <summary>
/// Publishes a notification to all subscribers of the specified event type.
/// </summary>
/// <typeparam name="TEvent">
/// The type of event to publish. This type should implement the INotification interface.
/// </typeparam>
public interface IPublisher<TEvent>
{
    /// <summary>
    /// Asynchonously publishes a notification to all subscribers of the specified event type.
    /// </summary>
    /// <typeparam name="TNotification">The type of notification to publish.</typeparam>
    /// <param name="notification">The actual notification to publish.</param>
    /// <param name="cancellationToken">An optional cancellation token</param>
    /// <returns>A task that represents the publish operation.</returns>
    public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default)
        where TNotification : INotification;
}
