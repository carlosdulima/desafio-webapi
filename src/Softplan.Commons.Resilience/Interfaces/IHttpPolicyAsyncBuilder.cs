using System;
using System.Threading;
using System.Threading.Tasks;

namespace Softplan.Commons.Resilience.Interfaces
{
    public interface IHttpPolicyAsyncBuilder
    {
        IHttpPolicyAsyncBuilder WithDefaultPolicies();
        IHttpPolicyAsyncBuilder WithRetry(int retryCount);
        IHttpPolicyAsyncBuilder WithWaitRetry(int retryCount, Func<int, TimeSpan> rettryAttempt);
        IHttpPolicyAsyncBuilder WithFallback(Func<CancellationToken, Task> fallbackAction);
        IHttpPolicyAsyncBuilder WithCircuitBreaker(TimeSpan rettryAttempt, int exceptionsAllowedBeforeBreaking = 3);
        IHttpPolicyAsyncBuilder WithCircuitBreaker(int exceptionsAllowedBeforeBreaking, TimeSpan rettryAttempt, Action<Exception, TimeSpan> onBreak, Action onReset, Action onHalfOpen);
        IHttpPolicyAsyncBuilder WithTimeout(TimeSpan timeout);
        IPolicyExecutor Build();
    }
}
