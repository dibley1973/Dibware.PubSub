namespace Dibware.PubSub.Core.Contracts;

/// <summary>
/// Defines the expected members of a simple mediator that can publish notifications to subscribers.
/// Currently this is only used for publishing notifications, but could be extended to support
/// requests and responses in the future.
/// </summary>>
public interface ISimpleMediator : IPublisher 
{ }
