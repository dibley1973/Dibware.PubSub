namespace Dibware.PubSub.Core.UnitTests.Tests.SimpleMediatorTests;

using Dibware.PubSub.Core.Contracts;
using Dibware.PubSub.Core.Extensions;
using Dibware.PubSub.Core.UnitTests.Fakes.Events;
using Microsoft.Extensions.DependencyInjection;
using Moq;

/// <summary>
/// Unit tests for the SimpleMediator class, specifically testing the parallel processing of notifications.
/// </summary>
/// <remarks>
/// In all of these tests the handlers are MOCKS and are explictly registered in the service collection. The SimpleMediator is configured to register notifications, but the handlers are not automatically discovered and registered.
/// This allows for precise control over which handlers are tested and ensures that the tests are isolated from any other handlers that may exist in the assembly.
/// </remarks>
[TestClass]
public sealed class SimpleMediatorTests_ParallelProcessing
{
    [TestMethod]
    public async Task Publish_CallsSingleHandler_WhenSingleHandlerIsRegistered()
    {
        // Arrange
        var services = new ServiceCollection();
        AddSimpleMediatorWithDefaultOptions(services);

        var notificationHandlerMock = new Mock<INotificationHandler<UserRegisteredEvent>>();

        notificationHandlerMock
            .Setup(handler => handler.Handle(It.IsAny<UserRegisteredEvent>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        _ = services.AddScoped<INotificationHandler<UserRegisteredEvent>>(provider => notificationHandlerMock.Object);

        var serviceProvider = services.BuildServiceProvider();
        var mediator = serviceProvider.GetRequiredService<ISimpleMediator>();
        var userRegisteredEvent = new UserRegisteredEvent("testuser", "testemail");

        // Act
        await mediator.Publish(userRegisteredEvent);

        // Assert
        notificationHandlerMock.Verify(
            handler => handler.Handle(It.IsAny<UserRegisteredEvent>(), It.IsAny<CancellationToken>()),
            Times.Once()
        );
    }

    [TestMethod]
    public async Task Publish_CallsAllHandlers_WhenThreeHandlersAreRegistered()
    {
        // Arrange
        var services = new ServiceCollection();
        AddSimpleMediatorWithDefaultOptions(services);

        var notificationHandlerMock1 = new Mock<INotificationHandler<UserRegisteredEvent>>();
        var notificationHandlerMock2 = new Mock<INotificationHandler<UserRegisteredEvent>>();
        var notificationHandlerMock3 = new Mock<INotificationHandler<UserRegisteredEvent>>();

        notificationHandlerMock1
            .Setup(handler => handler.Handle(It.IsAny<UserRegisteredEvent>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        notificationHandlerMock2
            .Setup(handler => handler.Handle(It.IsAny<UserRegisteredEvent>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        notificationHandlerMock3
            .Setup(handler => handler.Handle(It.IsAny<UserRegisteredEvent>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        _ = services.AddScoped<INotificationHandler<UserRegisteredEvent>>(provider => notificationHandlerMock1.Object);
        _ = services.AddScoped<INotificationHandler<UserRegisteredEvent>>(provider => notificationHandlerMock2.Object);
        _ = services.AddScoped<INotificationHandler<UserRegisteredEvent>>(provider => notificationHandlerMock3.Object);

        var serviceProvider = services.BuildServiceProvider();
        var mediator = serviceProvider.GetRequiredService<ISimpleMediator>();
        var userRegisteredEvent = new UserRegisteredEvent("testuser", "testemail");

        // Act
        await mediator.Publish(userRegisteredEvent);

        // Assert
        notificationHandlerMock1.Verify(
            handler => handler.Handle(It.IsAny<UserRegisteredEvent>(), It.IsAny<CancellationToken>()),
            Times.Once()
        );
        notificationHandlerMock2.Verify(
            handler => handler.Handle(It.IsAny<UserRegisteredEvent>(), It.IsAny<CancellationToken>()),
            Times.Once()
        );
        notificationHandlerMock3.Verify(
            handler => handler.Handle(It.IsAny<UserRegisteredEvent>(), It.IsAny<CancellationToken>()),
            Times.Once()
        );
    }

    [TestMethod]
    public async Task Publish_DoesNotCallHandler_WhenWrongHandlerIsRegistered()
    {
        // Arrange
        var services = new ServiceCollection();
        AddSimpleMediatorWithDefaultOptions(services);

        var notificationHandlerMock = new Mock<INotificationHandler<UserUnRegisteredEvent>>();

        notificationHandlerMock
            .Setup(handler => handler.Handle(It.IsAny<UserUnRegisteredEvent>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        _ = services.AddScoped<INotificationHandler<UserUnRegisteredEvent>>(provider => notificationHandlerMock.Object);

        var serviceProvider = services.BuildServiceProvider();
        var mediator = serviceProvider.GetRequiredService<ISimpleMediator>();
        var userRegisteredEvent = new UserRegisteredEvent("testuser", "testemail");

        // Act
        await mediator.Publish(userRegisteredEvent);

        // Assert
        notificationHandlerMock.Verify(
            handler => handler.Handle(It.IsAny<UserUnRegisteredEvent>(), It.IsAny<CancellationToken>()),
            Times.Never()
        );
    }

    [TestMethod]
    public async Task Publish_DoesNotCallAnyHandlers_WhenThreeIncorrectHandlersAreRegistered()
    {
        // Arrange
        var services = new ServiceCollection();
        AddSimpleMediatorWithDefaultOptions(services);

        var notificationHandlerMock1 = new Mock<INotificationHandler<UserRegisteredEvent>>();
        var notificationHandlerMock2 = new Mock<INotificationHandler<UserRegisteredEvent>>();
        var notificationHandlerMock3 = new Mock<INotificationHandler<UserRegisteredEvent>>();

        notificationHandlerMock1
            .Setup(handler => handler.Handle(It.IsAny<UserRegisteredEvent>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        notificationHandlerMock2
            .Setup(handler => handler.Handle(It.IsAny<UserRegisteredEvent>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        notificationHandlerMock3
            .Setup(handler => handler.Handle(It.IsAny<UserRegisteredEvent>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        _ = services.AddScoped<INotificationHandler<UserRegisteredEvent>>(provider => notificationHandlerMock1.Object);
        _ = services.AddScoped<INotificationHandler<UserRegisteredEvent>>(provider => notificationHandlerMock2.Object);
        _ = services.AddScoped<INotificationHandler<UserRegisteredEvent>>(provider => notificationHandlerMock3.Object);

        var serviceProvider = services.BuildServiceProvider();
        var mediator = serviceProvider.GetRequiredService<ISimpleMediator>();
        var userRegisteredEvent = new UserUnRegisteredEvent("testuser");

        // Act
        await mediator.Publish(userRegisteredEvent);

        // Assert
        notificationHandlerMock1.Verify(
            handler => handler.Handle(It.IsAny<UserRegisteredEvent>(), It.IsAny<CancellationToken>()),
            Times.Never()
        );
        notificationHandlerMock2.Verify(
            handler => handler.Handle(It.IsAny<UserRegisteredEvent>(), It.IsAny<CancellationToken>()),
            Times.Never()
        );
        notificationHandlerMock3.Verify(
            handler => handler.Handle(It.IsAny<UserRegisteredEvent>(), It.IsAny<CancellationToken>()),
            Times.Never()
        );
    }

    /// <summary>
    /// Adds the SimpleMediator to the service collection with default options for testing.
    /// Set for sequential processing and to register notifications.
    /// </summary>
    /// <param name="services"></param>
    private static void AddSimpleMediatorWithDefaultOptions(ServiceCollection services) =>
        services.AddSimpleMediator(options =>
        {
            options.RegisterNotificationsFromAssemblies = false;
            options.ProcessingMode = Registration.NotificationPublisherProcessingMode.Parallel;
        });
}
