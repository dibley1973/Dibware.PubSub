namespace Dibware.PubSub.Core.UnitTests.Tests;

using Dibware.PubSub.Core.Contracts;
using Dibware.PubSub.Core.UnitTests.Fakes.Events;
using Microsoft.Extensions.DependencyInjection;
using Moq;

[TestClass]
public sealed class SimpleMediatorTests
{
    [TestMethod]
    public async Task Publish_CallsSingleHandler_WhenSingleHandlerIsRegistered()
    {
        // Arrange
        var services = new ServiceCollection();

        var notificationHandlerMock = new Mock<INotificationHandler<UserRegisteredEvent>>();

        notificationHandlerMock
            .Setup(handler => handler.Handle(It.IsAny<UserRegisteredEvent>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        _ = services.AddScoped<INotificationHandler<UserRegisteredEvent>>(provider => notificationHandlerMock.Object);

        var serviceProvider = services.BuildServiceProvider();
        var mediator = serviceProvider.GetRequiredService<IPublisher<UserRegisteredEvent>>();
        var userRegisteredEvent = new UserRegisteredEvent("testuser", "testemail");

        // Act
        await mediator.Publish(userRegisteredEvent);

        // Assert
        notificationHandlerMock.Verify(
            handler => handler.Handle(It.IsAny<UserRegisteredEvent>(), It.IsAny<CancellationToken>()),
            Times.Once()
        );
    }
}
