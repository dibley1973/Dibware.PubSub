namespace Dibware.PubSub.Core.Registration;

using System;
using System.Collections.Generic;
using Dibware.PubSub.Core.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

/// <summary>
/// Provides methods for registering notification handlers in the service collection.
/// </summary>
internal static class ServiceRegistrator
{
    /// <summary>
    /// Registers notification handlers in the specified service collection based on the provided list of types and the notification handler type.
    /// </summary>
    /// <param name="services">The service collection to which the notification handlers will be added.</param>
    /// <param name="allHandlerTypes">The list of types to scan for notification handlers.</param>
    /// <param name="notificationHandlerType">The type of the notification handler interface.</param>
    public static void RegisterNotificationHandlers(IServiceCollection services, List<Type> allHandlerTypes, Type notificationHandlerType)
    {
        var allNotifications = allHandlerTypes
            .Where(IsConcreteClass())
            .SelectMany(handlerType => handlerType
                .GetInterfaces()
                .Where(IsGenericInterfaceWhichMatchesType(notificationHandlerType))
                .Select(@interface => new NotificationHandlerRegistration { HandlerType = handlerType, Interface = @interface }))
            .ToList();

        foreach (var notification in allNotifications)
        {
            var serviceType = notification.Interface;
            var implementationType = notification.HandlerType;

            if (serviceType.ContainsGenericParameters)
                serviceType = serviceType.GetGenericTypeDefinition();

            if (services.TypeIsAlreadyRegistered(serviceType))
                throw new InvalidOperationException($"Type of '{serviceType}' has already been registered");
            
            services.AddScoped(serviceType, implementationType);
        }
    }

    /// <summary>
    /// Gets a function that checks if a given type is a concrete class (i.e., not abstract).
    /// </summary>
    /// <returns>
    /// A function that takes a <see cref="Type"/> and returns <see langword="true"/>
    /// if it is a concrete class; otherwise, <see langword="false"/>.
    /// </returns>
    private static Func<Type, bool> IsConcreteClass() => handlerType => handlerType.IsConcreteClass();

    /// <summary>
    /// Gets a function that checks if a given interface type is a generic interface that matches the specified generic type definition.
    /// </summary>
    /// <param name="genericTypeDefinitionToMatch">
    /// The generic type definition to match against the interface type.
    /// </param>
    /// <returns>
    /// A function that takes a <see cref="Type"/> representing an interface and returns <see langword="true"/>
    /// if it matches the generic type definition; otherwise, <see langword="false"/>.
    /// </returns>
    private static Func<Type, bool> IsGenericInterfaceWhichMatchesType(Type genericTypeDefinitionToMatch) => @interface => @interface.IsGenericInterfaceWhichMatchesType(genericTypeDefinitionToMatch);
}
