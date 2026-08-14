namespace Dibware.PubSub.Core.UnitTests.Fakes.Events;

using Dibware.PubSub.Core.Contracts;

/// <summary>
/// Represents a fake event that is used to test the activity log handler.
/// </summary>
/// <param name="UserName">Represents the user name.</param>
public record UserUnRegisteredEvent(string UserName) : INotification;
