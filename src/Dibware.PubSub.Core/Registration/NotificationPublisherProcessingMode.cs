namespace Dibware.PubSub.Core.Registration;

/// <summary>
/// Indicates the processing mode for notification publishers, determining how notifications are handled by registered handlers.
/// </summary>
public enum NotificationPublisherProcessingMode
{
    /// <summary>
    /// Process notifications in parallel. This mode allows for concurrent execution of notification handlers.
    /// </summary>
    Parallel,

    /// <summary>
    /// Process notifications sequentially. This mode ensures that notification handlers are executed one after another.
    /// </summary>
    Sequential
}
