namespace Dibware.PubSub.Core.Contracts;

/// <summary>
/// Extension methods for the <see cref="CancellationToken"/> object.
/// </summary>
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
    /// <remarks>
    /// This code is courtesy of StephenCleary and his anser to a stack overflow question.
    /// REF: https://stackoverflow.com/a/27240225/254215
    /// 
    /// The original code can be found at the following link:
    /// REf: https://github.com/StephenCleary/AsyncEx/wiki/CancellationTokenExtensions
    /// </remarks>
    public static Task AsTask(this CancellationToken cancellationToken)
    {
        var tcs = new TaskCompletionSource<object>();
        cancellationToken.Register(() => tcs.TrySetCanceled(), useSynchronizationContext: false);
        return tcs.Task;
    }
}

