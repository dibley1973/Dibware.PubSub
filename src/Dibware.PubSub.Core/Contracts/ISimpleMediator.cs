namespace Dibware.PubSub.Core.Contracts;

/// <summary>
/// Defines the expected members of a simple mediator that can publish notifications to subscribers.
/// </summary>
/// <typeparam name="TNotification">
/// The type of notification that the mediator can publish. Must implement the <see cref="INotification"/> interface.
/// </typeparam>
public interface ISimpleMediator<TNotification> : IPublisher<TNotification>
    where TNotification : INotification
{ }
