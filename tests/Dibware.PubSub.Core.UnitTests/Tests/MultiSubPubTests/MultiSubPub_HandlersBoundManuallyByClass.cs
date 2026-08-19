namespace Dibware.PubSub.Core.UnitTests.Tests.MultiSubPubTests;

using Dibware.PubSub.Core.Contracts;
using Dibware.PubSub.Core.Extensions;
using Dibware.PubSub.Core.UnitTests.Fakes.Events;
using Dibware.PubSub.Core.UnitTests.Fakes.Handlers;
using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// unit tests for the 
/// </summary>
[TestClass]
public sealed class MultiSubPub_HandlersBoundManuallyByClass
{
    public TestContext TestContext { get; set; }

    [TestMethod]
    public async Task Publish_CallsSingleHandler_WhenSingleHandlerIsManuallyRegistered()
    {
        // Arrange
        var services = new ServiceCollection();
        AddMultiSubPubWithDefaultOptions(services);
        var serviceProvider = services.BuildServiceProvider();
        var pubSub = serviceProvider.GetRequiredService<ISubscribeAndPublish>();
        var userRegisteredEvent = new UserRegisteredEvent("test-user", "email-address");
        var userUnRegisteredEventNotificationHandler =
            (FakeUserRegisteredEventNotificationHandler)serviceProvider.GetRequiredService<INotificationHandler<UserRegisteredEvent>>();

        pubSub.Subscribe(userUnRegisteredEventNotificationHandler);

        // Act
        await pubSub.Publish(userRegisteredEvent, TestContext.CancellationToken);

        // Assert
        Assert.IsNotNull(userUnRegisteredEventNotificationHandler);
        Assert.IsTrue(userUnRegisteredEventNotificationHandler.HandleCalled);

        // Clean up
        pubSub.Unsubscribe(userUnRegisteredEventNotificationHandler);
    }

    private void AddMultiSubPubWithDefaultOptions(ServiceCollection services)
    {
        services.AddMultiSubPub();
        /*(options =>
        {
            options.NotificationRegistrationMode = NotificationRegistrationMode.ManualRegistration;
            options.ProcessingMode = Registration.NotificationPublisherProcessingMode.Sequential;
        });*/
    }
}
