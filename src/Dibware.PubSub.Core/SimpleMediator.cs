namespace Dibware.PubSub.Core;

using Dibware.PubSub.Core.Contracts;

// Ref: https://github.com/hasanxdev/DispatchR/blob/main/src/DispatchR/Mediator.cs
// Ref: https://github.com/LuckyPennySoftware/MediatR/blob/main/src/MediatR/Mediator.cs

public class SimpleMediator : ISimpleMediator<INotification>
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
    public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default)
        where TNotification : INotification
    {
        throw new NotImplementedException();
    }
}
