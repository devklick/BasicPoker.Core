namespace BasicPoker.Core.Extensions;

public static class TaskExtensions
{
    public static async Task<TResult> TimeoutAfterSeconds<TResult>(this Task<TResult> task, int seconds)
        => await task.TimeoutAfter(TimeSpan.FromSeconds(seconds));

    /// <summary>
    /// See: <see href="https://stackoverflow.com/a/22078975/6236042"></see>
    /// </summary>
    /// <param name="task"></param>
    /// <param name="timeout"></param>
    /// <typeparam name="TResult"></typeparam>
    /// <returns></returns>
    public static async Task<TResult> TimeoutAfter<TResult>(this Task<TResult> task, TimeSpan timeout)
    {
        using (var timeoutCancellationTokenSource = new CancellationTokenSource())
        {
            var completedTask = await Task.WhenAny(task, Task.Delay(timeout, timeoutCancellationTokenSource.Token));
            if (completedTask == task)
            {
                timeoutCancellationTokenSource.Cancel();
                return await task;  // Very important in order to propagate exceptions
            }
            else
            {
                throw new TimeoutException("The operation has timed out.");
            }
        }
    }
}