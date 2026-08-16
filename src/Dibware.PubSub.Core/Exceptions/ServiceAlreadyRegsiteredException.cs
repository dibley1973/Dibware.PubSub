namespace Dibware.PubSub.Core.Exceptions;

using System;
using System.Collections.Generic;
using System.Text;

public class ServiceAlreadyRegsiteredException : Exception
{
    /// <summary>
    /// Creates a new instance of the <see cref="ServiceAlreadyRegsiteredException"/> class.
    /// </summary>
    public ServiceAlreadyRegsiteredException() : base() { }

    /// <summary>
    /// Creates a new instance of the <see cref="ServiceAlreadyRegsiteredException"/> class.
    /// </summary>
    /// <param name="serviceType">
    /// The type of the service to register. Typically this will be an interface or abstract class.
    /// </param>
    /// <param name="implementationType">The implementation type of the service.</param>
    public ServiceAlreadyRegsiteredException(Type serviceType, Type implementationType)
        : base($"""
            A service has already been registered for service type '{serviceType.Name}' against implementation type '{implementationType.Name}'.
            Only one service can be registered for a given service type. Please check for multiple concrete classes
            which implement an interface, or inherif from a base class of type '{serviceType.Name}'.
            """) { }

    /// <summary>
    /// Creates a new instance of the <see cref="ServiceAlreadyRegsiteredException"/> class.
    /// </summary>
    /// <param name="message"></param>
    public ServiceAlreadyRegsiteredException(string message) : base(message) { }
}
