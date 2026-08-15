namespace Dibware.PubSub.Core.Extensions;

using System;
using System.Collections.Generic;
using Dibware.PubSub.Core.Contracts;
using Dibware.PubSub.Core.Registration;
using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Provides extension methods for adding the SimpleMediator to an IServiceCollection.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds the SimpleMediator to the service collection using configuration built by the specified action.
    /// </summary>
    /// <param name="services">
    /// The service collection to which the SimpleMediator will be added.
    /// </param>
    /// <param name="buildConfiguration">
    /// An action that builds the configuration options for the SimpleMediator.
    /// </param>
    /// <returns>
    /// The updated service collection with the SimpleMediator added.
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
        services.AddScoped<ISimpleMediator, SimpleMediator>();

        services.AddNotificationPublisher(configurationOptions);

        var syncNotificationHandlerType = typeof(INotificationHandler<>);

        var handlerTypes = new HashSet<Type>()
        {
            syncNotificationHandlerType
        };

        var allTypes = configurationOptions.Assemblies.SelectMany(x => x.GetTypes()).Distinct()
            .Where(type =>
            {
                var genericInterfaces = type.GetInterfaces()
                    .Where(i => i.IsGenericType)
                    .Select(i => i.GetGenericTypeDefinition())
                    .ToList();

                return genericInterfaces.Intersect(handlerTypes).Any();
            })
            .ToList();

        if (configurationOptions.RegisterNotifications)
            ServiceRegistrator.RegisterNotification(services, allTypes, syncNotificationHandlerType);

        return services;
    }

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
        if (configurationOptions.ProcessingMode == NotificationPublisherProcessingMode.Sequential)
            services.AddScoped<INotificationPublisher, SequentialPublisher>();
        else if (configurationOptions.ProcessingMode == NotificationPublisherProcessingMode.Parallel)
            services.AddScoped<INotificationPublisher, ParallelPublisher>();
        else
            throw new ArgumentOutOfRangeException(nameof(configurationOptions.ProcessingMode), configurationOptions.ProcessingMode, "Invalid processing mode.");
    }
}
