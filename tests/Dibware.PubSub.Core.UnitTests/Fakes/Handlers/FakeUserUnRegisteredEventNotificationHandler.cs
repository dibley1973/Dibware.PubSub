namespace Dibware.PubSub.Core.UnitTests.Fakes.Handlers;

using Dibware.PubSub.Core.Contracts;
using Dibware.PubSub.Core.UnitTests.Fakes.Events;

/// <summary>
/// Represents a fake notification handler for the UserUnRegisteredEvent used in unit tests.
/// Once the Handle method is called, the HandleCalled property will be set to true.
/// </summary>
public class FakeUserUnRegisteredEventNotificationHandler : INotificationHandler<UserUnRegisteredEvent>
{
    /// <summary>
    /// Indicates whether the Handle method has been called. This property starts as <see langword="false"/>,
    /// and is set to <see langword="true"/> when the Handle method is invoked.
    /// </summary>
    public bool HandleCalled { get; private set; } = false;

    /// <summary>
    /// Handles the UserUnRegisteredEvent notification. When this method is called, it sets the HandleCalled property to <see langword="true"/>.
    /// </summary>
    /// <param name="_notification">
    /// The UserUnRegisteredEvent notification that is being handled. This parameter is not used in the method,
    /// but it is required by the INotificationHandler interface.
    /// </param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task Handle(UserUnRegisteredEvent _notification, CancellationToken cancellationToken)
    {
        HandleCalled = true;

        return Task.CompletedTask;
    }
}
