namespace Dibware.PubSub.Core.Extensions;

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Dibware.PubSub.Core.Contracts;
using Dibware.PubSub.Core.HandlerDiscovery;
using Dibware.PubSub.Core.Registration;
using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Provides extension methods for adding the SimpleMediator to an IServiceCollection.
/// </summary>
public static class ServiceCollectionExtensions_SimpleMediator
{
    /// <summary>
    /// Represents the type of the notification handler interface, which is used to identify and register notification handlers in the service collection.
    /// </summary>
    public static readonly Type NotificationHandlerType = typeof(INotificationHandler<>);

    /// <summary>
    /// Represents a set of handler types that are used to identify and register various types of handlers in the service collection.
    /// </summary>
    public static readonly HashSet<Type> HandlerTypes = new()
    {
        NotificationHandlerType
        // We can add more handler types here in the future if needed. I.e. IRequestHandler<>, etc.
    };

    /// <summary>
    /// Adds the <see cref="SimpleMediator"/> to the service collection using configuration built by the specified action.
    /// </summary>
    /// <param name="services">
    /// The service collection to which the <see cref="SimpleMediator"/> will be added.
    /// </param>
    /// <param name="buildConfiguration">
    /// An action that builds the configuration options for the SimpleMediator.
    /// </param>
    /// <returns>
    /// The updated service collection with the <see cref="SimpleMediator"/> added.
    /// </returns>
    public static IServiceCollection AddSimpleMediator(this IServiceCollection services, Action<ConfigurationOptions> buildConfiguration)
    {
        var config = new ConfigurationOptions();
        buildConfiguration(config);

        return services.AddSimpleMediator(config);
    }

    /// <summary>
    /// Adds the SimpleMediator to the service collection using the specified configuration options.
    /// </summary>
    /// <param name="services">
    /// The service collection to which the SimpleMediator will be added.
    /// </param>
    /// <param name="configurationOptions">
    /// The configuration options for the SimpleMediator.
    /// </param>
    /// <returns>
    /// The updated service collection with the SimpleMediator added.
    /// </returns>
    public static IServiceCollection AddSimpleMediator(this IServiceCollection services, ConfigurationOptions configurationOptions)
    {
        services
            .AddScoped<ISimpleMediator, SimpleMediator>()
            .AddNotificationPublisher(configurationOptions);

        // Discover all notification handlers based on the specified notification registration mode set in the configuration options.
        var notificationHandlerDiscovery = NotificationHandlerDiscoveryFactory.Create(configurationOptions.NotificationRegistrationMode);
        var allHandlerTypes = notificationHandlerDiscovery
            .DiscoverNotificationHandlers(configurationOptions, HandlerTypes)
            .ToList();

        // We only register notification handlers if the registration mode is set to either FromAssemblies or FromTypes.
        // There is no need to register notification handlers if the registration mode is set to ManualRegistration,
        // as it implies that the user will handle the registration manually.
        if (configurationOptions.NotificationRegistrationMode == NotificationRegistrationMode.RegisterFromAssemblies ||
            configurationOptions.NotificationRegistrationMode == NotificationRegistrationMode.RegisterFromTypes)
        {
            ServiceRegistrator.RegisterNotificationHandlers(services, allHandlerTypes, NotificationHandlerType);
        }

        return services;
    }

    /// <summary>
    /// Gets a value indicating if the specified type is already
    /// </summary>
    /// <param name="services">
    /// The service collection which iss to be checked.
    /// </param>
    /// <param name="type">the <see cref="Type"/> to check for</param>
    /// <returns>
    /// Returns <see langword="true"/> of the type has already been added.
    /// </returns>
    public static bool TypeIsAlreadyRegistered(this IServiceCollection services, Type type) =>
        services.Any(x => x.ServiceType == type);

    /// <summary>
    /// Adds the appropriate notification publisher to the service collection based on the specified configuration options.
    /// Interogates the <see cref="ConfigurationOptions.ProcessingMode"/> setting to determine whether to register a
    /// sequential or parallel notification publisher.
    /// </summary>
    /// <param name="services">
    /// The service collection to which the notification publisher will be added.
    /// </param>
    /// <param name="configurationOptions">
    /// The configuration options for the notification publisher.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when the <see cref="ConfigurationOptions.ProcessingMode"/> is set to an invalid value.
    /// </exception>
    private static void AddNotificationPublisher(this IServiceCollection services, ConfigurationOptions configurationOptions)
    {
        _ = configurationOptions.ProcessingMode switch
        {
            NotificationPublisherProcessingMode.Sequential => services.AddScoped<INotificationPublisher, SequentialPublisher>(),
            NotificationPublisherProcessingMode.Parallel => services.AddScoped<INotificationPublisher, ParallelPublisher>(),
            _ => throw new ArgumentOutOfRangeException(nameof(configurationOptions.ProcessingMode), configurationOptions.ProcessingMode, "Invalid processing mode.")
        };
    }
}
