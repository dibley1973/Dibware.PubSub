namespace Dibware.PubSub.Core.Registration;

using System.Reflection;

/// <summary>
/// Represents configuration options for the PubSub components.
/// </summary>
public class ConfigurationOptions
{
    /// <summary>
    /// Set to <see langword="true"/> to register notification handlers. Default is <see langword="true"/>.
    /// </summary>
    public bool RegisterNotifications { get; set; } = true;

    /// <summary>
    /// Gets the list of assemblies to scan for handlers and other components.
    /// </summary>
    public List<Assembly> Assemblies { get; } = new();

    /// <summary>
    /// Gets or sets the processing mode for notification publishers. 
    /// Default is <see cref="NotificationPublisherProcessingMode.Sequential"/>.
    /// </summary>
    public NotificationPublisherProcessingMode ProcessingMode { get; set; } = NotificationPublisherProcessingMode.Sequential;
}
