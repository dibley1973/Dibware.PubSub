namespace Dibware.PubSub.Core;

using System;
using System.Collections.Generic;
using System.Text;
using Dibware.PubSub.Core.Contracts;

/// <summary>
/// A simple multiple-subscription publishing component.
/// </summary>
public class MultiSubPub : ISubscribeAndPublish
{
    /// <summary>
    /// Asynchonously publishes a notification to all subscribers of the specified event type.
    /// </summary>
    /// <typeparam name="TNotification">
    /// The type of notification to publish. Must implement <see cref="INotification"/>.
    /// </typeparam>
    /// <param name="notification">The actual notification to publish.</param>
    /// <param name="cancellationToken">An optional cancellation token</param>
    /// <returns>A task that represents the publish operation.</returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default)
        where TNotification : INotification
    {
        ArgumentNullException.ThrowIfNull(notification, nameof(notification));

        throw new NotImplementedException();
    }

    /// <summary>
    /// Subscribes the specified handler.
    /// </summary>
    /// <typeparam name="TNotification">
    /// The type of the notification. Must implement <see cref="INotification"/>.
    /// </typeparam>
    /// <param name="handler">
    /// The handler to subscribe.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Thrown if the value of <paramref name="handler"/> is <see langword="null"/>.
    /// </exception>
    public void Subscribe<TNotification>(INotificationHandler<TNotification> handler)
        where TNotification : INotification
    {
        ArgumentNullException.ThrowIfNull(handler);

        throw new NotImplementedException();
    }

    /// <summary>
    /// Unsubscribes the specified handler.
    /// </summary>
    /// <typeparam name="TNotification">
    /// The type of the notification. Must implement <see cref="INotification"/>.
    /// </typeparam>
    /// <param name="handler">
    /// The handler to unsubscribe.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Thrown if the value of <paramref name="handler"/> is <see langword="null"/>.
    /// </exception>
    /// <exception cref="NotImplementedException"></exception>
    public void Unsubscribe<TNotification>(INotificationHandler<TNotification> handler)
        where TNotification : INotification
    {
        ArgumentNullException.ThrowIfNull(handler);

        throw new NotImplementedException();
    }
}
