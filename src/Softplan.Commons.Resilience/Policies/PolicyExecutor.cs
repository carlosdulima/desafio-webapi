using Polly;
using Polly.Registry;
using Softplan.Commons.Resilience.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Softplan.Commons.Resilience.Policies
{
    public class PolicyExecutor : IPolicyExecutor
    {
        public PolicyRegistry PolicyRegistry { get; set; }

        public PolicyExecutor(IEnumerable<IAsyncPolicy> policies)
        {
            var policiesAsync = policies ?? throw new ArgumentNullException(nameof(policies));

            PolicyRegistry = new PolicyRegistry
            {
                [nameof(PolicyExecutor)] = Policy.WrapAsync(policiesAsync.ToArray())
            };
        }

        public async Task ExecuteAsync(Func<Task> action)
        {
            var policy = PolicyRegistry.Get<IAsyncPolicy>(nameof(PolicyExecutor));
            await policy.ExecuteAsync(action);
        }

        public async Task<T> ExecuteAsync<T>(Func<Task<T>> action)
        {
            var policy = PolicyRegistry.Get<IAsyncPolicy>(nameof(PolicyExecutor));
            return await policy.ExecuteAsync(action);
        }

        public async Task ExecuteAsync(Func<Context, Task> action, Context context)
        {
            var policy = PolicyRegistry.Get<IAsyncPolicy>(nameof(PolicyExecutor));
            await policy.ExecuteAsync(action, context);
        }
    }
}
