namespace Dibware.PubSub.Core.UnitTests.Tests.SimpleMediatorTests;

using System.Reflection;
using Dibware.PubSub.Core.Contracts;
using Dibware.PubSub.Core.Extensions;
using Dibware.PubSub.Core.Registration;
using Dibware.PubSub.Core.UnitTests.Fakes.Events;
using Dibware.PubSub.Core.UnitTests.Fakes.Handlers;
using Microsoft.Extensions.DependencyInjection;

[TestClass]
public sealed class SimpleMediatorTests_HandlersBoundAutomaticallyByAssembly
{
    [TestMethod]
    public async Task Publish_CallsSingleHandler_WhenSingleHandlerIsAutomaticallyRegistered()
    {
        // Arrange
        var services = new ServiceCollection();
        AddSimpleMediatorWithDefaultOptions(services);

        var serviceProvider = services.BuildServiceProvider();
        var mediator = serviceProvider.GetRequiredService<ISimpleMediator>();
        var userRegisteredEvent = new UserUnRegisteredEvent("testuser");
        var userUnRegisteredEventNotificationHandler =
            (FakeUserUnRegisteredEventNotificationHandler)serviceProvider.GetRequiredService<INotificationHandler<UserUnRegisteredEvent>>();

        // Act
        await mediator.Publish(userRegisteredEvent);

        // Assert
        Assert.IsNotNull(userUnRegisteredEventNotificationHandler);
        Assert.IsTrue(userUnRegisteredEventNotificationHandler.HandleCalled);
    }

    /// <summary>
    /// Adds the SimpleMediator to the service collection with default options for testing.
    /// Set for sequential processing and to register notifications.
    /// </summary>
    /// <param name="services"></param>
    private static void AddSimpleMediatorWithDefaultOptions(ServiceCollection services)
    {
        var assembliesToAdd = new List<Assembly>() { typeof(FakeUserUnRegisteredEventNotificationHandler).Assembly };

        services.AddSimpleMediator(options =>
        {
            options.AssembliesToScanForNotifications.AddRange(assembliesToAdd);
            options.ProcessingMode = Registration.NotificationPublisherProcessingMode.Sequential;
            options.NotificationRegistrationMode = NotificationRegistrationMode.FromAssemblies;
        });
    }
}
