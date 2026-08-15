namespace Dibware.PubSub.Core.Contracts;

using static System.Runtime.InteropServices.JavaScript.JSType;

/// <summary>
/// Uses Task.WhenAll with the list of Handler tasks to process notifications in parallel:
/// <code>
/// var tasks =
///     handlers
///         .Select(handler => handler.Handle(notification, cancellationToken))
///         .ToList();
/// 
/// return Task.WhenAll(tasks);
/// </code>
/// </summary>
public class ParallelPublisher : INotificationPublisher
{
    /// <summary>
    /// Publishes a notification to one or more registered handlers by using Task.WhenAll with the list of Handler tasks.
    /// </summary>
    /// <param name="handlerExecutors">
    /// A collection of registered handlers that will be invoked to handle the notification.
    /// </param>
    /// <param name="notification">
    /// The notification to be published to the registered handlers.
    /// </param>
    /// <param name="cancellationToken">
    /// A token to cancel the asynchronous operation.
    /// </param>
    /// <returns></returns>
    public Task Publish(IEnumerable<INotificationHandler<INotification>> handlerExecutors, INotification notification, CancellationToken cancellationToken)
    {
        var tasks = handlerExecutors
            .Select(handler => handler.Handle(notification, cancellationToken))
            .ToArray();

        return Task.WhenAny(Task.WhenAll(tasks), cancellationToken.AsTask());

        // Apparently, the line below will also perform the same as the line above,
        // but without using the .AsTask() extension method. WaitAsync is available in .NET 6.0 and later,
        // so if you are using an earlier version, you may need to use the AsTask() method instead.
        //return Task.WhenAll(tasks).WaitAsync(cancellationToken);
    }
}
