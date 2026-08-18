namespace Dibware.PubSub.Core.Contracts;

/// <summary>
/// Defines the expected members of a component that takes subscriptions and can publish notofications.
/// </summary>
public interface ISubscribeAndPublish : ISubscriber, IPublisher;
