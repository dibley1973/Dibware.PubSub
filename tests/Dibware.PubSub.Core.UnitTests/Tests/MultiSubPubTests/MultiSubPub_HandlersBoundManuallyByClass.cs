namespace Dibware.PubSub.Core.UnitTests.Tests.MultiSubPubTests;

using Dibware.PubSub.Core.Extensions;
using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// unit tests for the 
/// </summary>
[TestClass]
public sealed class MultiSubPub_HandlersBoundManuallyByClass
{
    [TestMethod]
    public async Task Publish_CallsSingleHandler_WhenSingleHandlerIsManuallyRegistered()
    {

        // Arrange
        var services = new ServiceCollection();
        AddMultiSubPubWithDefaultOptions(services);
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
