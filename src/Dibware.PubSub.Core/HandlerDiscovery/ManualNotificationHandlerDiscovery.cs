using Dibware.PubSub.Core.Contracts;
using Dibware.PubSub.Core.Registration;

namespace Dibware.PubSub.Core.HandlerDiscovery;

/// <summary>
/// Represents a manual discovery mechanism for notification handlers, allowing for explicit
/// registration of handlers without relying on automatic assembly scanning or type discovery.
/// </summary>
public class ManualNotificationHandlerDiscovery : INotificationHandlerDiscovery
{
    /// <summary>
    /// Discovers and returns a collection of notification handler types that are registered within the system.
    /// for manual registration, this method returns an empty collection, as no automatic discovery is performed.
    /// </summary>
    /// <param name="configurationOptions">
    /// The configuration options for the SimpleMediator.
    /// </param>
    /// <param name="handlerTypes">
    /// A set of <see cref="Type"/> representing the notification handler types to be discovered.
    /// </param>
    /// <returns>
    /// An enumerable collection of <see cref="Type"/> representing the discovered notification handler types.
    /// In the case of manual registration, this collection will always be empty, indicating that no handlers are automatically discovered.
    /// </returns>
    public IEnumerable<Type> DiscoverNotificationHandlers(ConfigurationOptions configurationOptions, HashSet<Type> handlerTypes) => new List<Type>();
}
