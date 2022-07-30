using System;
using System.Threading;

namespace ThreadPoolExercises.Core
{
    public class ThreadingHelpers
    {
        public static void ExecuteOnThread(Action action, int repeats, CancellationToken token = default,
            Action<Exception>? errorAction = null)
        {
            // * Create a thread and execute there `action` given number of `repeats` - waiting for the execution!
            //   HINT: you may use `Join` to wait until created Thread finishes
            // * In a loop, check whether `token` is not cancelled
            // * If an `action` throws and exception (or token has been cancelled) - `errorAction` should be invoked (if provided)

            for (int i = 0; i < repeats; i++)
            {
                Exception? exception = null;
                var thread = new Thread(() => DoAction(action, out exception));
                thread.Start();
                thread.Join();
                if (token.IsCancellationRequested || exception != null)
                {
                    errorAction?.Invoke(exception!);
                    return;
                }
            }
        }

        public static void ExecuteOnThreadPool(Action action, int repeats, CancellationToken token = default,
            Action<Exception>? errorAction = null)
        {
            // * Queue work item to a thread pool that executes `action` given number of `repeats` - waiting for the execution!
            //   HINT: you may use `AutoResetEvent` to wait until the queued work item finishes
            // * In a loop, check whether `token` is not cancelled
            // * If an `action` throws and exception (or token has been cancelled) - `errorAction` should be invoked (if provided)

            for (int i = 0; i < repeats; i++)
            {
                AutoResetEvent done = new AutoResetEvent(false);
                Exception? exception = null;
                ThreadPool.QueueUserWorkItem(state =>
                {
                    DoAction(action, out exception);
                    done.Set();
                });
                done.WaitOne();
                if (token.IsCancellationRequested || exception != null)
                {
                    errorAction?.Invoke(exception!);
                    return;
                }
            }
        }

        private static void DoAction(Action action, out Exception? exception)
        {
            try
            {
                action();
                exception = null!;
            }
            catch (Exception? e)
            {
                exception = e;
            }
        }
    }
}