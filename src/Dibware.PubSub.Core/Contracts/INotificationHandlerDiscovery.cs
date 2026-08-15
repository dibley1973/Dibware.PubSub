namespace Dibware.PubSub.Core.Contracts;

using Dibware.PubSub.Core.Registration;

/// <summary>
/// Defines a contract for discovering notification handlers within the system.
/// </summary>
public interface INotificationHandlerDiscovery
{
    /// <summary>
    /// Discovers and returns a collection of notification handler types that are registered within the system.
    /// </summary>
    /// <param name="configurationOptions">
    /// The configuration options for the SimpleMediator.
    /// </param>
    /// <param name="handlerTypes">
    /// A set of <see cref="Type"/> representing the notification handler types to be discovered.
    /// </param>
    /// <returns>
    /// An enumerable collection of <see cref="Type"/> representing the discovered notification handler types.
    /// </returns>
    public IEnumerable<Type> DiscoverNotificationHandlers(ConfigurationOptions configurationOptions, HashSet<Type> handlerTypes);
}
