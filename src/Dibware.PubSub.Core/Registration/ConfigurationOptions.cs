namespace Dibware.PubSub.Core.Registration;

using System.Reflection;

/// <summary>
/// Represents configuration options for the PubSub components.
/// </summary>
public class ConfigurationOptions
{
    /// <summary>
    /// Set to <see langword="true"/> to register notification handlers from assemblies. Default is <see langword="false"/>.
    /// Ensure if this property is set to <see langword="true"/>, that the <see cref="AssembliesToScanForNotifications"/>
    /// property is populated with the assemblies containing the notification handlers.
    /// </summary>
    public bool RegisterNotificationsFromAssemblies { get; set; } = false;

    /// <summary>
    /// Gets the list of assemblies to scan for handlers and other components.
    /// Ensure that <see cref="RegisterNotificationsFromAssemblies"/> is set to <see langword="true"/>,
    /// if assemblies containing the notification handlers are included in this list.
    /// Defaults to an empty list.
    /// </summary>
    public List<Assembly> AssembliesToScanForNotifications { get; set; } = new();

    /// <summary>
    /// Gets or sets the processing mode for notification publishers. 
    /// Default is <see cref="NotificationPublisherProcessingMode.Sequential"/>.
    /// </summary>
    public NotificationPublisherProcessingMode ProcessingMode { get; set; } = NotificationPublisherProcessingMode.Sequential;
}
