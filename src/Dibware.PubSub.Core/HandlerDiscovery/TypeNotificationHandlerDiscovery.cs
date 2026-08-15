using Dibware.PubSub.Core.Contracts;
using Dibware.PubSub.Core.Registration;

namespace Dibware.PubSub.Core.HandlerDiscovery;

/// <summary>
/// Represents a discovery mechanism for notification handlers that scans a provided list of types.
/// </summary>
public class TypeNotificationHandlerDiscovery : INotificationHandlerDiscovery
{
    /// <summary>
    /// Discovers notification handler types from the specified list of types based on the provided configuration options and handler types.
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
    public IEnumerable<Type> DiscoverNotificationHandlers(ConfigurationOptions configurationOptions, HashSet<Type> handlerTypes)
    {
        var allHandlerTypes = configurationOptions.TypesToRegisterForNotifications
           .Where(type =>
           {
               var genericInterfaces = type.GetInterfaces()
                   .Where(i => i.IsGenericType)
                   .Select(i => i.GetGenericTypeDefinition())
                   .ToList();

               return genericInterfaces.Intersect(handlerTypes).Any();
           })
           .ToList();

        return allHandlerTypes;
    }
}
