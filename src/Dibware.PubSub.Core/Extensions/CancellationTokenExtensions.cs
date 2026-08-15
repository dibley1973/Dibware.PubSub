namespace Dibware.PubSub.Core.Contracts;

public static class CancellationTokenExtensions
{
    /// <summary>
    /// Creates a Task that completes when the CancellationToken is canceled.
    /// </summary>
    /// <param name="cancellationToken">
    /// The CancellationToken to convert to a Task.
    /// </param>
    /// <returns>
    /// A Task that completes when the CancellationToken is canceled.
    /// </returns>
    public static Task AsTask(this CancellationToken cancellationToken)
    {
        var tcs = new TaskCompletionSource<object>();
        cancellationToken.Register(() => tcs.TrySetCanceled(), useSynchronizationContext: false);
        return tcs.Task;
    }
}

