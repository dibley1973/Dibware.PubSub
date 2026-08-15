namespace Dibware.PubSub.Core.Registration;

/// <summary>
/// Indicates the mode of registration for notification handlers, specifying how they
/// should be discovered and registered within the system.
/// The default is <see cref="ManualRegistration"/>.
/// </summary>
public enum NotificationRegistrationMode
{
    /// <summary>
    /// Register notification handlers manually. This mode indicates that the component should not automatically
    /// register handlers from assemblies or types, and that registration will be handled manually by the user.
    /// This is the default mode.
    /// </summary>
    ManualRegistration = 0,

    /// <summary>
    /// Register notification handlers from assemblies. This mode indicates the component
    /// should scan specified assemblies for notification handlers.
    /// </summary>
    FromAssemblies = 1,

    /// <summary>
    /// Register notification handlers from a list of types. This mode indicates the component
    /// should register the specified types as notification handlers.
    /// </summary>
    FromTypes = 2
}
