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

### Examples

#### Using defaul processing mode (sequential):

```C#
services.AddSimpleMediator(options =>
    {
        options.RegisterNotifications = true;
    });
```

##### Using parallel processing mode:

```C#
services.AddSimpleMediator(options =>
    {
        options.RegisterNotifications = true;
        options.ProcessingMode = Registration.NotificationPublisherProcessingMode.Parallel;
    });
```

## Configuration Options

The confuguration options are available via the `ConfigurationOptions` class. The following options are available:

### RegisterNotifications

- Type: `bool`
- Default: `false`

When set to `true`, the `SimpleMediator` will automatically register all notification handlers that are found in the assembly. 
This is useful if you want to use the `SimpleMediator` as a notification bus.

### ProcessingMode

- Type: `NotificationPublisherProcessingMode`
- Default: `NotificationPublisherProcessingMode.Sequential`

Sets the processing mode for notification handlers. The following options are available:

- `Sequential`: Notification handlers will be executed sequentially, one after the other.
- `Parallel`: Notification handlers will be executed in parallel, using `Task.WhenAll`.
