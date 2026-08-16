namespace Dibware.PubSub.Core.UnitTests.Tests.SimpleMediatorTests;

using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Xml.Linq;
using Dibware.PubSub.Core.Contracts;
using Dibware.PubSub.Core.Exceptions;
using Dibware.PubSub.Core.Extensions;
using Dibware.PubSub.Core.Registration;
using Dibware.PubSub.Core.UnitTests.Fakes.Events;
using Dibware.PubSub.Core.UnitTests.Fakes.Handlers;
using Microsoft.Extensions.DependencyInjection;
using static System.Net.Mime.MediaTypeNames;

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
        var userRegisteredEvent = new UserRegisteredEvent("test-user", "email-address");
        var userUnRegisteredEventNotificationHandler =
            (FakeUserRegisteredEventNotificationHandler)serviceProvider.GetRequiredService<INotificationHandler<UserRegisteredEvent>>();

        // Act
         await mediator.Publish(userRegisteredEvent);

        // Assert
        Assert.IsNotNull(userUnRegisteredEventNotificationHandler);
        Assert.IsTrue(userUnRegisteredEventNotificationHandler.HandleCalled);
    }

    [TestMethod]
    [Ignore("Uncomment 'SecondFakeUserUnRegisteredEventNotificationHandler' to run this test.")]
    public async Task AddSimpleMediator_ThrowsExceptionWhen_WhentwoHandlersForSameEventAreAutomaticallyRegistered()
    {
        // Arrange
        var services = new ServiceCollection();
        var assembliesToAdd = new List<Assembly>() { typeof(FakeUserUnRegisteredEventNotificationHandler).Assembly };

        // Act
        Action act = () => services.AddSimpleMediator(options =>
        {
            options.AssembliesToScanForNotifications.AddRange(assembliesToAdd);
            options.ProcessingMode = Registration.NotificationPublisherProcessingMode.Sequential;
            options.NotificationRegistrationMode = NotificationRegistrationMode.RegisterFromAssemblies;
        });

        // Assert
        Assert.ThrowsExactly<ServiceAlreadyRegsiteredException>(act);
    }

    /// <summary>
    /// Adds the SimpleMediator to the service collection with default options for testing.
    /// Set for sequential processing and to register notifications.
    /// </summary>
    /// <param name="services"></param>
    private static void AddSimpleMediatorWithDefaultOptions(ServiceCollection services)
    {
        var assembliesToAdd = new List<Assembly>() { typeof(FakeUserRegisteredEventNotificationHandler).Assembly };

        services.AddSimpleMediator(options =>
        {
            options.AssembliesToScanForNotifications.AddRange(assembliesToAdd);
            options.ProcessingMode = Registration.NotificationPublisherProcessingMode.Sequential;
            options.NotificationRegistrationMode = NotificationRegistrationMode.RegisterFromAssemblies;
        });
    }
}
