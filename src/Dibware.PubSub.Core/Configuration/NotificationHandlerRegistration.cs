namespace Dibware.PubSub.Core.Configuration;

using System;

/// <summary>
/// Represents a registration of a notification handler, containing the handler type and the corresponding interface type.
/// </summary>
/// <param name="HandlerType">
/// The type of the notification handler.
/// </param>
/// <param name="Interface">
/// The type of the interface implemented by the notification handler.
/// </param>
public record struct NotificationHandlerRegistration(Type HandlerType, Type Interface);
