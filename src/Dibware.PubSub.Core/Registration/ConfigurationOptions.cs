namespace Dibware.PubSub.Core.Registration;

using System.Reflection;

/// <summary>
/// Represents configuration options for the PubSub components.
/// </summary>
public class ConfigurationOptions
{
    /// <summary>
    /// Gets or sets the list of assemblies to scan for handlers and other components.
    /// Ensure that <see cref="NotificationRegistrationMode"/> is set to <see cref="NotificationRegistrationMode.RegisterFromAssemblies"/>,
    /// if assemblies containing the notification handlers are included in this list.
    /// Defaults to an empty list.
    /// </summary>
    public List<Assembly> AssembliesToScanForNotifications { get; set; } = new();

    /// <summary>
    /// Gets or sets the list of types to register for notifications.
    /// Ensure that <see cref="NotificationRegistrationMode"/> is set to  <see cref="NotificationRegistrationMode.RegisterFromTypes"/>,
    /// if types which are the registered as notification handlers are included in this list.
    /// Defaults to an empty list.
    /// </summary>
    public List<Type> TypesToRegisterForNotifications { get; set; } = new();

    /// <summary>
    /// Gets or sets the mode of registration for notification handlers, specifying how they should be discovered and registered within the system.
    /// The default is <see cref="NotificationRegistrationMode.ManualRegistration"/>.
    /// <para>
    /// This property allows you to control the registration behavior of notification handlers,
    /// enabling you to choose between manual registration, assembly scanning, or type-based registration.
    /// </para>
    /// <para>
    /// When set to <see cref="NotificationRegistrationMode.ManualRegistration"/>, the system will not automatically
    /// register handlers from assemblies or types, and you will need to handle registration manually.
    /// </para>
    /// <para>
    /// When set to <see cref="NotificationRegistrationMode.RegisterFromAssemblies"/>, the system will scan the assemblies specified
    /// in <see cref="AssembliesToScanForNotifications"/> for notification handlers and register them automatically.
    /// </para>
    /// <para>
    /// When set to <see cref="NotificationRegistrationMode.RegisterFromTypes"/>, the system will register the types specified
    /// in <see cref="TypesToRegisterForNotifications"/> as notification handlers automatically.
    /// </para>
    /// </summary>
    public NotificationRegistrationMode NotificationRegistrationMode { get; set; } = NotificationRegistrationMode.ManualRegistration;

    /// <summary>
    /// Gets or sets the processing mode for notification publishers. 
    /// Default is <see cref="NotificationPublisherProcessingMode.Sequential"/>.
    /// </summary>
    public NotificationPublisherProcessingMode ProcessingMode { get; set; } = NotificationPublisherProcessingMode.Sequential;
}
