namespace Dibware.PubSub.Core;

using Dibware.PubSub.Core.Contracts;
using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// A simple mediator implementation that allows for publishing notifications to registered handlers.
/// </summary>
/// <remarks>
/// Reference repositories for inspiration and implementation details:
/// Ref: https://github.com/hasanxdev/DispatchR/blob/main/src/DispatchR/Mediator.cs
/// Ref: https://github.com/LuckyPennySoftware/MediatR/blob/main/src/MediatR/Mediator.cs
/// </remarks>
public class SimpleMediator : ISimpleMediator
{
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// Initializes a new instance of the <see cref="SimpleMediator"/> class.
    /// </summary>
    /// <param name="serviceProvider">Service provider. Can be a scoped or root provider</param>
    public SimpleMediator(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

    /// <summary>
    /// Publishes a notification to all registered handlers.
    /// </summary>
    /// <typeparam name="TNotification">The type of the notification.</typeparam>
    /// <param name="notification">The notification to publish.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <exception cref="NotImplementedException">Thrown when the method is not implemented.</exception>
    public async Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default)
        where TNotification : INotification
    {
        var notificationHandlers = _serviceProvider
            .GetRequiredService<IEnumerable<INotificationHandler<TNotification>>>()
            .ToArray();

        // TODO:Investigate if using Unsafe.As below is actully needed in our implementation.
        // It is used in the DispatchR implementation, but we are not sure if it is needed in our case.
        //var unSafeHandlers = Unsafe.As<INotificationHandler<TNotification>[]>(handlers);

        foreach (var notificationHandler in notificationHandlers)
        {
            if (cancellationToken.IsCancellationRequested)
                break; // Dont handle any more notifications if cancellation is requested

            var handlerTask = notificationHandler.Handle(notification, cancellationToken);

            if (!handlerTask.IsCompletedSuccessfully)
                await handlerTask;
        }
    }
}
