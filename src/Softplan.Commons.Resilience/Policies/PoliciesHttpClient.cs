using Polly;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Softplan.Commons.Resilience.Policies
{
    public static class PoliciesHttpClient
    {
        public static IAsyncPolicy RetryPolicyAsync(int retryCount) =>
            Policies<HttpRequestException>.RetryPolicyAsync(retryCount);

        public static IAsyncPolicy WaitRetryPolicyAsync(int retryCount, Func<int, TimeSpan> rettryAttempt) =>
            Policies<HttpRequestException>.WaitRetryPolicyAsync(retryCount, rettryAttempt);

        public static IAsyncPolicy CircuitBreakerPolicyAsync(int exceptionsAllowedBeforeBreaking, TimeSpan rettryAttempt) =>
            Policies<HttpRequestException>.CircuitBreakerPolicyAsync(exceptionsAllowedBeforeBreaking, rettryAttempt);

        public static IAsyncPolicy CircuitBreakerPolicyAsync(int exceptionsAllowedBeforeBreaking, TimeSpan rettryAttempt, Action<Exception, TimeSpan> onBreak, Action onReset, Action onHalfOpen) =>
            Policies<HttpRequestException>.CircuitBreakerPolicyAsync(exceptionsAllowedBeforeBreaking, rettryAttempt, onBreak, onReset, onHalfOpen);

        public static IAsyncPolicy FallBackPolicyAsync(Func<CancellationToken, Task> fallbackAction) =>
            Policies<HttpRequestException>.FallBackPolicyAsync(fallbackAction);

        public static IAsyncPolicy TimeOutPolicyAsync(TimeSpan timeout) =>
         Policies<HttpRequestException>.TimeOutPolicyAsync(timeout);
    }
}
