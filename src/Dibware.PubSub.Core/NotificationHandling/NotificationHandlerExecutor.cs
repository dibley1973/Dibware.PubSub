using Dibware.PubSub.Core.Contracts;

namespace Dibware.PubSub.Core.NotificationHandling;

/// <summary>
/// Represents an executor for a notification handler, encapsulating the handler instance and its callback function.
/// <para>
/// This is used to facilitate the execution of notification handlers in a generic manner.
/// <para>
/// </summary>
/// <typeparam name="TNotification">
/// The type of notification that the handler is designed to process. Must implement the <see cref="INotification"/> interface.
/// </typeparam>
/// <param name="HandlerInstance">
/// The instance of the notification handler that will be invoked to handle the notification. This instance is expected to
/// implement the appropriate notification handler interface for the specified notification type.
/// </param>
/// <param name="HandlerCallback">
/// The callback function that will be invoked to handle the notification. This function takes the notification
/// and cancellation token as parameters and returns a task representing the asynchronous operation.
/// </param>
public record NotificationHandlerExecutor<TNotification>(object HandlerInstance, Func<TNotification, CancellationToken, Task> HandlerCallback)
    where TNotification : INotification;
