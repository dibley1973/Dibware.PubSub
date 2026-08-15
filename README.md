# Dibware.PubSub

This is a simple pub/sub implementation for .NET applications. It allows you to easily publish messages to subscribers and manage subscriptions.
Yes, this is yet another pub/sub implementation for .NET applications! Am I reinventing the wheel? 
Almost certainly, but I wanted to create a simple implementation that is easy for me to use and for me to understand.
I also wanted to improve my knowledge of the pub/sub pattern and how it can be implemented in .NET applications.
At the timeof writing my role has moved away from software engineering and into a more integration based role, so I wanted to keep my skills sharp and continue to learn.

this library has taken a lot of inspiration from the following libraries:

- [MediatR](https://github.com/LuckyPennySoftware/MediatR)
- [DispatchR](https://github.com/hasanxdev/DispatchR)

At the point of writing both libraries have far more fuinctionailty than this library, but the driver is to create a simple implementation that is easy to use and understand.
In addition it appears the later version of MediatR require licensing for commercial use, which is not the case for this library. 
This is a key driver for me, as the organisation I work for has a number of internal applications that require pub/sub functionality, and this library is developed being used in those applications.

## Getting Started

TBC.

## Registering with IServiceCollection

Dibware.PubSub supports `Microsoft.Extensions.DependencyInjection.Abstractions` directly. To register `Dibware.PubSub.SimpleMediator` services and handlers:

### Notification Registration Mode

There are three available notification registration modes for notification handlers: two automatic and one manual. The default registration mode is manual.

#### Automatic Registration Mode

The two automatic modes are using assembly scanning to find and register notification handlers, or speccifying the notification handler types explicitly.

Set `NotificationRegistrationMode` to `RegisterFromAssemblies` to use automatic registration mode with assembly scanning.

```C#
services.AddSimpleMediator(options =>
    {
        options.NotificationRegistrationMode = Registration.NotificationRegistrationMode.RegisterFromAssemblies;
    });
```

Set `NotificationRegistrationMode` to `RegisterFromTypes` to use automatic registration mode with explicit type registration.

```C#
services.AddSimpleMediator(options =>
    {
        options.NotificationRegistrationMode = Registration.NotificationRegistrationMode.RegisterFromTypes;
    });
```

#### Manual Registration Mode

Set `NotificationRegistrationMode` to `ManualRegistration` to use manual registration mode.

```C#
services.AddSimpleMediator(options =>
    {
        options.NotificationRegistrationMode = Registration.NotificationRegistrationMode.ManualRegistration;
    });
```
If manual mode is set then the system requires the user to register notification handlers manually.

```C#
 services.AddScoped(typeof(INotificationHandler<UserUnRegisteredEvent>), typeof(FakeUserUnRegisteredEventNotificationHandler));
```

### Processing Mode

There are two available processing modes for notification handlers: sequential and parallel. The default processing mode is sequential.

#### Using sequential processing mode (the default mode):

```C#
services.AddSimpleMediator(options =>
    {
        options.ProcessingMode = Registration.NotificationPublisherProcessingMode.Sequential;
    });
```

##### Using parallel processing mode:

```C#
services.AddSimpleMediator(options =>
    {
        options.ProcessingMode = Registration.NotificationPublisherProcessingMode.Parallel;
    });
```

## Configuration Options

The confuguration options are available via the `ConfigurationOptions` class. The following options are available:

### AssembliesToScanForNotifications

- Type: `List<Assembly>`
- Default: Empty collection

### TypesToRegisterForNotifications

- Type: `List<Type>`
- Default: Empty collection


### NotificationRegistrationMode

- Type: `NotificationRegistrationMode` (enum)
- Default: `ManualRegistration`

Sets the registration mode for notification handlers. The following options are available:

- `ManualRegistration`: Notification handlers must be registered manually.
- `RegisterFromAssemblies`: Notification handlers will be automatically registered from the specified assemblies.
- `RegisterFromTypes`: Notification handlers will be automatically registered from the specified types.

### ProcessingMode

- Type: `NotificationPublisherProcessingMode` (enum)
- Default: `NotificationPublisherProcessingMode.Sequential`

Sets the processing mode for notification handlers. The following options are available:

- `Sequential`: Notification handlers will be executed sequentially, one after the other.
- `Parallel`: Notification handlers will be executed in parallel, using `Task.WhenAll`.
