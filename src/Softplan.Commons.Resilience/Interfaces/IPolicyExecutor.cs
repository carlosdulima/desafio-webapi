using Polly;
using Polly.Registry;
using System;
using System.Threading.Tasks;

namespace Softplan.Commons.Resilience.Interfaces
{
    public interface IPolicyExecutor
    {
        PolicyRegistry PolicyRegistry { get; set; }
        Task ExecuteAsync(Func<Task> action);
        Task<T> ExecuteAsync<T>(Func<Task<T>> action);
        Task ExecuteAsync(Func<Context, Task> action, Context context);
    }
}
