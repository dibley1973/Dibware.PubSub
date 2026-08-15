using Dibware.PubSub.Core.Contracts;
using Dibware.PubSub.Core.Registration;

namespace Dibware.PubSub.Core.HandlerDiscovery;

/// <summary>
/// Factory class for creating instances of <see cref="INotificationHandlerDiscovery"/>
/// based on the specified <see cref="NotificationRegistrationMode"/>.
/// </summary>
public static class NotificationHandlerDiscoveryFactory
{
    /// <summary>
    /// Creates an instance of <see cref="INotificationHandlerDiscovery"/> based on the specified <see cref="NotificationRegistrationMode"/>.
    /// </summary>
    /// <param name="registrationMode"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static INotificationHandlerDiscovery Create(NotificationRegistrationMode registrationMode)
    {
        return registrationMode switch
        {
            NotificationRegistrationMode.ManualRegistration => new ManualNotificationHandlerDiscovery(),
            NotificationRegistrationMode.FromAssemblies => new AssemblyNotificationHandlerDiscovery(),
            NotificationRegistrationMode.FromTypes => new TypeNotificationHandlerDiscovery(),
            _ => throw new ArgumentOutOfRangeException(nameof(registrationMode), registrationMode, null)
        };
    }
}
