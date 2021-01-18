using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SearchAggregator.Services
{
    public static class TaskExtension
    {
        public static Task<Task<T>> WhenFirst<T>(IEnumerable<Task<T>> tasks, Func<Task<T>, bool> predicate)
        {
            var tasksArray = (tasks as IReadOnlyList<Task<T>>) ?? tasks.ToArray();

            var tcs = new TaskCompletionSource<Task<T>>();
            var count = tasksArray.Count;

            Action<Task<T>> continuation = t =>
            {
                if (predicate(t))
                {
                    tcs.TrySetResult(t);
                }
                if (Interlocked.Decrement(ref count) == 0)
                {
                    tcs.TrySetResult(null);
                }
            };

            foreach (var task in tasksArray)
            {
                task.ContinueWith(continuation);
            }

            return tcs.Task;
        }
    }
}
