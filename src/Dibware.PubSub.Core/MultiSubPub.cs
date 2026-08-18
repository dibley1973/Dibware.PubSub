namespace Dibware.PubSub.Core;

using System;
using System.Collections.Generic;
using System.Text;
using Dibware.PubSub.Core.Contracts;

/// <summary>
/// A simple multiple-subscription publishing component.
/// </summary>
public class MultiSubPub : ISubscribeAndPublish
{
    public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default) where TNotification : INotification => throw new NotImplementedException();
}
